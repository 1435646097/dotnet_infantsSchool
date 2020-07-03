using AutoMapper;
using Common.Tools;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [ApiController]
    [Authorize("actionAuthrization")]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<MessageModel<IEnumerable<MenuDto>>>> GetMenu()
        {
            MessageModel<IEnumerable<MenuDto>> res = new MessageModel<IEnumerable<MenuDto>>();
            string uid = HttpContext.User.Claims.Where(c => c.Type == "id").FirstOrDefault().Value;
            User entity = await _userServices.GetEntityByIdAsync(Convert.ToInt32(uid));
            var rids = entity.UserRole.Select(r => r.RoleId);
            var aids = _roleActionServices.GetEntitys().Where(r => rids.Contains(r.RoleId)).Select(r => r.ActionId);
            var actionParents = _actionServices.GetEntitys().Where(a => aids.Contains(a.Id) && a.Pid == 0).OrderBy(a => a.OrderBy);
            IEnumerable<MenuDto> menuDtoList = _mapper.Map<IEnumerable<MenuDto>>(actionParents);
            foreach (var item in menuDtoList)
            {
                var actionChildren = _actionServices.GetEntitys().Where(a => a.Pid == item.Id && aids.Contains(a.Id));
                item.Children = _mapper.Map<IEnumerable<MenuDto>>(actionChildren);
            }
            res.Data = menuDtoList;
            return Ok(res);
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="actionParams">参数</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetActionList))]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetActionList([FromQuery] ActionParams actionParams)
        {
            MessageModel<IEnumerable<ActionDto>> res = new MessageModel<IEnumerable<ActionDto>>();
            PagedList<Model.Entitys.Action> list = await _actionServices.GetActionPaged(actionParams);
            //配置x-pagination响应头
            string previousLink = list.HasPrevious ? CreateLink(PagedType.Previous, actionParams) : null;
            string nextLink = list.HasNext ? CreateLink(PagedType.Next, actionParams) : null;
            var pagination = new
            {
                currentPage = actionParams.PageNum,
                totalPage = list.TotalPage,
                totalCount = list.TotalCount,
                previousLink,
                nextLink
            };
            HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
            res.Data = _mapper.Map<IEnumerable<ActionDto>>(list);
            return Ok(res);
        }
        /// <summary>
        /// 获取权限列表的树状结构
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetActionTree()
        {
            MessageModel<IEnumerable<MenuDto>> res = new MessageModel<IEnumerable<MenuDto>>();
            //获得所有的一级权限
            var oneActions = await _actionServices.GetEntitys().Where(a => a.Pid == 0 && a.IsDelete == false).OrderBy(a => a.OrderBy).ToListAsync();
            IEnumerable<MenuDto> oneActionDtos = _mapper.Map<IEnumerable<MenuDto>>(oneActions);
            foreach (var one in oneActionDtos)
            {
                //获得所有的二级权限
                var twoActions = _actionServices.GetEntitys().Where(a => a.Pid == one.Id);
                one.Children = _mapper.Map<IEnumerable<MenuDto>>(twoActions);
                foreach (var three in one.Children)
                {
                    //获取所有的三级权限
                    var threeActions = _actionServices.GetEntitys().Where(a => a.Pid == three.Id);
                    three.Children = _mapper.Map<IEnumerable<MenuDto>>(threeActions);
                }
            }
            res.Data = oneActionDtos;
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<MessageModel<IEnumerable<MenuDto>>>> GetOneTwoActionTree()
        {
            MessageModel<IEnumerable<MenuDto>> res = new MessageModel<IEnumerable<MenuDto>>();
            var actionParents = await _actionServices.GetEntitys().Where(a => a.ActionTypeId == 1).OrderBy(a => a.OrderBy).ToListAsync();
            IEnumerable<MenuDto> menuDtoList = _mapper.Map<IEnumerable<MenuDto>>(actionParents);
            foreach (var item in menuDtoList)
            {
                var actionChildren = await _actionServices.GetEntitys().Where(a => a.Pid == item.Id).ToListAsync();
                item.Children = _mapper.Map<IEnumerable<MenuDto>>(actionChildren);
            }
            res.Data = menuDtoList;
            return Ok(res);
        }
        /// <summary>
        /// 根据id查询权限
        /// </summary>
        /// <param name="id">权限id</param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        /// <summary>
        /// 根据角色id查询权限
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<MessageModel<IEnumerable<int>>>> GetActionByRoleId(int id)
        {
            MessageModel<IEnumerable<int>> res = new MessageModel<IEnumerable<int>>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色编号";
                res.Success = false;
                return Ok(res);
            }
            Role entity = await _roleServices.GetEntityByIdAsync(id);
            IEnumerable<int?> actionIds = entity.RoleAction.Select(a => a.ActionId);
            List<Model.Entitys.Action> actionList = await _actionServices.GetEntitys().Where(a => actionIds.Contains(a.Id)).OrderBy(a => a.OrderBy).ToListAsync();
            List<int> ids = new List<int>();
            List<int> oneActionIds = actionList.Where(a => a.Pid == 0).Select(a => a.Id).ToList();
            List<int> twoActionIds = actionList.Where(a => oneActionIds.Contains((int)a.Pid)).Select(a => a.Id).ToList();
            foreach (var item in oneActionIds)
            {
                var temp = actionList.Where(a => a.Pid == item).FirstOrDefault();
                if (temp == null)
                {
                    ids.Add(item);
                }
            }
            foreach (var item in twoActionIds)
            {
                var temp = actionList.Where(a => a.Pid == item).FirstOrDefault();
                if (temp == null)
                {
                    ids.Add(item);
                }
            }
            List<int> threeActionIds = actionList.Where(a => twoActionIds.Contains((int)a.Pid)).Select(a => a.Id).ToList();
            ids.AddRange(threeActionIds);
            res.Data = ids;
            return Ok(res);
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="actionAddDto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据角色id设置其权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>

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
        /// <summary>
        /// 删除角色下面的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>

        [HttpDelete("{roleId}/Action/{actionId}")]
        public async Task<ActionResult<MessageModel<ActionTreeDto>>> DeleteActionByRoleId(int roleId, int actionId)
        {
            MessageModel<IEnumerable<ActionTreeDto>> res = new MessageModel<IEnumerable<ActionTreeDto>>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == roleId);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色编号";
                res.Success = false;
                return Ok(res);
            }
            RoleAction roleAction = await _roleActionServices.GetEntitys().Where(a => a.RoleId == roleId && a.ActionId == actionId).FirstOrDefaultAsync();
            await _roleActionServices.DeleteEntityAsync(roleAction);
            List<int?> aIds = await _roleActionServices.GetEntitys().Where(a => a.RoleId == roleId).Select(a => a.ActionId).ToListAsync();
            List<Model.Entitys.Action> actions = await _actionServices.GetEntitys().Where(a => aIds.Contains(a.Id)).OrderBy(a => a.OrderBy).ToListAsync();
            List<ActionTreeDto> actionTreeDtos = _mapper.Map<List<ActionTreeDto>>(actions);
            ActionTreeDto rootRoot = new ActionTreeDto
            {
                Id = 0,
                Pid = 0,
                Name = "根节点"
            };
            RecursionHelper.LoopToAppendChildren(actionTreeDtos, rootRoot, 0);
            res.Data = actionTreeDtos.Where(a => a.Pid == 0).ToList();
            return Ok(res);
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="actionDto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<MessageModel<UserDto>>> GetUserInfo()
        {
            MessageModel<UserDto> res = new MessageModel<UserDto>();
            string uid = HttpContext.User.Claims.Where(c => c.Type == "id").FirstOrDefault().Value;
            Model.Entitys.User entity = await _userServices.GetEntityByIdAsync(Convert.ToInt32(uid));
            UserDto dto = _mapper.Map<UserDto>(entity);
            res.Data = dto;
            return Ok(res);
        }
    }
}