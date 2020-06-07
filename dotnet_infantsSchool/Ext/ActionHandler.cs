using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Ext
{
    public class ActionHandler : AuthorizationHandler<ActionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IActionServices _actionServices;
        private readonly IRoleActionServices _roleActionServices;

        public ActionHandler(IHttpContextAccessor httpContextAccessor, IUserRoleServices userRoleServices, IActionServices actionServices, IRoleActionServices roleActionServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRoleServices = userRoleServices;
            _actionServices = actionServices;
            _roleActionServices = roleActionServices;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActionRequirement requirement)
        {
            HttpContext _httpContext = _httpContextAccessor.HttpContext;
            //获取用户的Id
            Claim uIdClaim = _httpContext.User.Claims.Where(a => a.Type == "id").FirstOrDefault();
            if (uIdClaim == null)
            {
                context.Fail();
                return;
            }
            int uId = Convert.ToInt32(uIdClaim.Value);
            //获取用户所有的角色Id
            List<int?> roleIds = await _userRoleServices.GetEntitys().Where(u => u.AccountId == uId).Select(u => u.RoleId).ToListAsync();
            //获取用户所有的权限Id
            List<int?> actionIds = await _roleActionServices.GetEntitys().Where(a => roleIds.Contains(a.RoleId)).Select(a => a.ActionId).ToListAsync();
            //获取用户所有的权限
            List<Model.Entitys.Action> actionList = await _actionServices.GetEntitys().Where(a => actionIds.Contains(a.Id)).ToListAsync();
            string path = _httpContext.Request.Path.Value.ToLower().Substring(4);
            string method = _httpContext.Request.Method.ToLower();
            foreach (var item in actionList)
            {
                if (path.Contains(item.Path.ToLower()) && item.Method?.ToLower() == method)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }
    }
}
