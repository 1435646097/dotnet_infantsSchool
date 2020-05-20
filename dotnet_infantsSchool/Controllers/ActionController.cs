using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using Newtonsoft.Json;
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
            var actionParents = _actionServices.GetEntitys().Where(a => aids.Contains(a.Id) && a.Pid == 0);
            IEnumerable<MenuDto> menuDtoList = _mapper.Map<IEnumerable<MenuDto>>(actionParents);
            foreach (var item in menuDtoList)
            {
                var actionChildren = _actionServices.GetEntitys().Where(a => a.Pid == item.Id && aids.Contains(a.Id));
                item.Children = _mapper.Map<IEnumerable<MenuDto>>(actionChildren);
            }
            res.Data = menuDtoList;
            return Ok(res);
        }
        [HttpGet(Name = nameof(GetActionList))]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetActionList([FromQuery]ActionParams actionParams)
        {
            MessageModel<IEnumerable<ActionDto>> res = new MessageModel<IEnumerable<ActionDto>>();
            PagedList<Model.Entitys.Action> list = await _actionServices.GetActionPaged(actionParams);
            //配置x-pagination响应头
            string previousLink = list.HasPrevious ? CreateLink(PagedType.Previous, actionParams) : null;
            string nextLink = list.HasNext ? CreateLink(PagedType.Next, actionParams) : null;
            var pagination = new
            {
                currentPage = list.PageNum,
                totalPage = list.TotalPage,
                totalCount = list.TotalCount,
                previousLink,
                nextLink
            };
            HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
            res.Data = _mapper.Map<IEnumerable<ActionDto>>(list);
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ActionDto>> GetActionById(int id)
        {
            MessageModel<ActionDto> res = new MessageModel<ActionDto>();
            bool result = await _actionServices.ExistEntityAsync(a => a.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色编号";
                res.Success = false;
                return Ok(res);
            }
            Model.Entitys.Action entity = await _actionServices.GetEntityByIdAsync(id);
            res.Data = _mapper.Map<ActionDto>(entity);
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
        [HttpPost]
        public async Task<ActionResult<MessageModel<ActionDto>>> AddAction(ActionAddDto actionAddDto)
        {
            MessageModel<ActionDto> res = new MessageModel<ActionDto>();
            Model.Entitys.Action entity = _mapper.Map<Model.Entitys.Action>(actionAddDto);
            await _actionServices.AddEntityAsync(entity);
            res.Code = 201;
            res.Data = _mapper.Map<ActionDto>(entity);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageModel<string>>> DeleteAction(int id)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _actionServices.ExistEntityAsync(a => a.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                return Ok(res);
            }
            await _actionServices.DeleteAction(id);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<MessageModel<string>>> EditAction(ActionDto actionDto)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _actionServices.ExistEntityAsync(a => a.Id == actionDto.Id);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                return Ok(res);
            }
            Model.Entitys.Action entity = _mapper.Map<Model.Entitys.Action>(actionDto);
            await _actionServices.EditEntityAsync(entity);
            return Ok(res);
        }
        private string CreateLink(PagedType pagedType, ActionParams actionParams)
        {
            switch (pagedType)
            {
                case PagedType.Previous:
                    return Url.Link(nameof(GetActionList), new
                    {
                        PageNum = actionParams.PageNum - 1,
                        PageSize = actionParams.PageSize,
                        Name = actionParams.Name,
                        Level = actionParams.Level
                    });

                case PagedType.Next:
                    return Url.Link(nameof(GetActionList), new
                    {
                        PageNum = actionParams.PageNum + 1,
                        PageSize = actionParams.PageSize,
                        Name = actionParams.Name,
                        Level = actionParams.Level
                    });
            }
            return string.Empty;
        }
    }
}
