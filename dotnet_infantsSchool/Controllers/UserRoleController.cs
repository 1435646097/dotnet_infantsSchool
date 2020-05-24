using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/userRole/{accountId}")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IMapper _mapper;

        public UserRoleController(IUserServices userServices, IUserRoleServices userRoleServices, IMapper mapper)
        {
            _userServices = userServices;
            _userRoleServices = userRoleServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<UserRoleDto>>>> GetUserRole(int accountId)
        {
            MessageModel<IEnumerable<UserRoleDto>> res = new MessageModel<IEnumerable<UserRoleDto>>();
            IEnumerable<UserRole> userRoles = await _userRoleServices.GetEntitys().Where(u => u.AccountId == accountId).ToListAsync();
            res.Data = _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<MessageModel<string>>> AddUserRole(int accountId, IEnumerable<int> roleIds)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _userServices.ExistEntityAsync(u => u.Id == accountId);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                res.Msg = "账户Id不正确，请重新输入";
                return Ok(res);
            }
            await _userRoleServices.AddUserRoles(accountId, roleIds);
            res.Msg = "成功！！!";
            return Ok(res);
        }
    }
}
