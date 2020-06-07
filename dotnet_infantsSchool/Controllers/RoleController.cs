using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/role")]
    [Authorize("actionAuthrization")]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleServices _roleServices;
        private readonly IRoleActionServices _roleActionServices;

        public RoleController(IMapper mapper, IRoleServices roleServices, IRoleActionServices roleActionServices)
        {
            _mapper = mapper;
            _roleServices = roleServices;
            _roleActionServices = roleActionServices;
        }
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<RoleDto>>>> GetRole()
        {
            MessageModel<IEnumerable<RoleDto>> res = new MessageModel<IEnumerable<RoleDto>>();
            List<Role> roles = await _roleServices.GetEntitys().ToListAsync();
            IEnumerable<RoleDto> rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            res.Data = rolesDto;
            return Ok(res);
        }
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
