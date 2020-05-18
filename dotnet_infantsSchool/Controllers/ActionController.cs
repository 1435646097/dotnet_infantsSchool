using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ActionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleServices _roleServices;
        private readonly IUserServices _userServices;
        private readonly IRoleActionServices _roleActionServices;
        private readonly IActionServices _actionServices;

        public ActionController(IMapper mapper, IRoleServices roleServices, IUserServices userServices, IRoleActionServices roleActionServices, IActionServices actionServices)
        {
            _mapper = mapper;
            _roleServices = roleServices;
            _userServices = userServices;
            _roleActionServices = roleActionServices;
            _actionServices = actionServices;
        }
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<MenuDto>>>> GetMenu()
        {
            MessageModel<IEnumerable<MenuDto>> res = new MessageModel<IEnumerable<MenuDto>>();
            string uid = HttpContext.User.Claims.Where(c => c.Type == "id").FirstOrDefault().Value;
            User entity = await _userServices.GetEntityByIdAsync(Convert.ToInt32(uid));
            var rids = entity.UserRole.Select(r => r.RoleId);
            var aids = _roleActionServices.GetEntitys().Where(r => rids.Contains(r.RoleId)).Select(r => r.ActionId);
            var actionParents = _actionServices.GetEntitys().Where(a => aids.Contains(a.Id));
            IEnumerable<MenuDto> menuDtoList = _mapper.Map<IEnumerable<MenuDto>>(actionParents);
            foreach (var item in menuDtoList)
            {
                var actionChildren = _actionServices.GetEntitys().Where(a => a.Pid == item.Id);
                item.Children = _mapper.Map<IEnumerable<MenuDto>>(actionChildren);
            }
            res.Data = menuDtoList;
            return Ok(res);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetActionList()
        {
            MessageModel<IEnumerable<ActionDto>> res = new MessageModel<IEnumerable<ActionDto>>();
            IEnumerable<Model.Entitys.Action> actions = await _actionServices.GetEntitys().ToListAsync();
            res.Data = _mapper.Map<IEnumerable<ActionDto>>(actions);
            return Ok(res);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetActionTree()
        {
            MessageModel<IEnumerable<MenuDto>> res = new MessageModel<IEnumerable<MenuDto>>();
            var actionParents = await _actionServices.GetEntitys().Where(a => a.Pid == 0 && a.IsDelete == false).ToListAsync();
            IEnumerable<MenuDto> menuDtoList = _mapper.Map<IEnumerable<MenuDto>>(actionParents);
            foreach (var item in menuDtoList)
            {
                var actionChildren = _actionServices.GetEntitys().Where(a => a.Pid == item.Id);
                item.Children = _mapper.Map<IEnumerable<MenuDto>>(actionChildren);
            }
            res.Data = menuDtoList;
            return Ok(res);
        }
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<int>>>> GetActionByRoleId(int id)
        {
            MessageModel<IEnumerable<int?>> res = new MessageModel<IEnumerable<int?>>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色编号";
                res.Success = false;
                return Ok(res);
            }
            Role entity = await _roleServices.GetEntityByIdAsync(id);
            IEnumerable<int?> actionIds = entity.RoleAction.Where(a => a.Action.Pid != 0).Select(a => a.ActionId);
            res.Data = actionIds;
            return Ok(res);
        }
        [HttpPost("{roleId}")]
        public async Task<ActionResult<MessageModel<string>>> SetActionByRoleId(int roleId, IEnumerable<int> actionId)
        {
            MessageModel<IEnumerable<string>> res = new MessageModel<IEnumerable<string>>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == roleId);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色编号";
                res.Success = false;
                return Ok(res);
            }
            await _actionServices.SetRoleAction(roleId, actionId);
            return Ok(res);
        }
    }
}
