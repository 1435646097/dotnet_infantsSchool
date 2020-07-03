using AutoMapper;
using Common.Tools;
using IServices;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiController]
    [Route("api/role")]
    [Authorize("actionAuthrization")]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleServices _roleServices;
        private readonly IRoleActionServices _roleActionServices;
        private readonly IActionServices _actionServices;

        public RoleController(IMapper mapper, IRoleServices roleServices, IRoleActionServices roleActionServices, IActionServices actionServices)
        {
            _mapper = mapper;
            _roleServices = roleServices;
            _roleActionServices = roleActionServices;
            _actionServices = actionServices;
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<RoleDto>>>> GetRole()
        {
            MessageModel<IEnumerable<RoleDto>> res = new MessageModel<IEnumerable<RoleDto>>();
            List<Role> roleList = await _roleServices.GetEntitys().ToListAsync();
            List<RoleDto> roleDtos = _mapper.Map<List<RoleDto>>(roleList);
            for (int i = 0; i < roleList.Count; i++)
            {
                List<int?> aIds = roleList[i].RoleAction.Select(a => a.ActionId).ToList();
                List<Model.Entitys.Action> actions = await _actionServices.GetEntitys().Where(a => aIds.Contains(a.Id)).ToListAsync();
                List<ActionTreeDto> actionTreeDtos = _mapper.Map<List<ActionTreeDto>>(actions);
                ActionTreeDto rootRoot = new ActionTreeDto
                {
                    Id = 0,
                    Pid = 0,
                    Name = "根节点"
                };
                RecursionHelper.LoopToAppendChildren(actionTreeDtos, rootRoot, 0);
                roleDtos[i].Children = actionTreeDtos.Where(a => a.Pid == 0).ToList();
            }
            res.Data = roleDtos;
            return Ok(res);
        }
        /// <summary>
        /// 根据id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel<RoleDto>>> GetRoleById(int id)
        {
            MessageModel<RoleDto> res = new MessageModel<RoleDto>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色id";
                res.Success = false;
                return Ok(res);
            }
            Role role = await _roleServices.GetEntityByIdAsync(id);
            RoleDto roleDto = _mapper.Map<RoleDto>(role);
            res.Data = roleDto;
            return Ok(res);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MessageModel<RoleDto>>> addRole(RoleDto roleDto)
        {
            MessageModel<RoleDto> res = new MessageModel<RoleDto>();
            Role entity = _mapper.Map<Role>(roleDto);
            await _roleServices.AddEntityAsync(entity);
            res.Data = _mapper.Map<RoleDto>(entity);
            res.Code = 201;
            return Ok(res);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<MessageModel<RoleDto>>> EditRole(RoleDto roleDto)
        {
            MessageModel<RoleDto> res = new MessageModel<RoleDto>();
            Role entity = _mapper.Map<Role>(roleDto);
            await _roleServices.EditEntityAsync(entity);
            res.Data = _mapper.Map<RoleDto>(entity);
            res.Code = 201;
            return Ok(res);
        }
        /// <summary>
        /// 根据id删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageModel<string>>> deleteRole(int id)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _roleServices.ExistEntityAsync(r => r.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的角色id";
                res.Data = "删除角色失败";
                res.Success = false;
                return Ok(res);
            }
            Role entity = await _roleServices.GetEntityByIdAsync(id);
            await _roleServices.DeleteRoleAndAction(id);
            res.Data = "删除角色成功";
            return Ok(res);
        }
    }
}