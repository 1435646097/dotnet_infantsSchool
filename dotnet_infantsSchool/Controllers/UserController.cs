using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetUser))]
        public async Task<ActionResult<IEnumerable<MessageModel<UserDto>>>> GetUser([FromQuery]UserParams userParams)
        {
            MessageModel<IEnumerable<UserDto>> res = new MessageModel<IEnumerable<UserDto>>();
            PagedList<User> list = await _userServices.GetUserPagedAsync(userParams);
            //配置x-pagination响应头
            string previousLink = list.HasPrevious ? CreateLink(PagedType.Previous, userParams) : null;
            string nextLink = list.HasNext ? CreateLink(PagedType.Next, userParams) : null;
            var pagination = new
            {
                currentPage = list.PageNum,
                totalPage = list.TotalPage,
                totalCount = list.TotalCount,
                previousLink,
                nextLink
            };
            HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
            //映射dto
            IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(list);

            res.Data = userDtos;
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel<UserDto>>> getUserById(int id)
        {
            MessageModel<UserDto> res = new MessageModel<UserDto>();
            bool result = await _userServices.ExistEntityAsync(a => a.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                res.Msg = "请输入正确的账户ID";
                return Ok(res);
            }
            Model.Entitys.User entity = await _userServices.GetEntityByIdAsync(id);
            res.Data = _mapper.Map<UserDto>(entity);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<MessageModel<UserDto>>> AddUser(UserAddDto userAddDto)
        {
            MessageModel<UserDto> res = new MessageModel<UserDto>();
            bool result = await _userServices.ExistEntityAsync(u => u.Account == userAddDto.Account);
            if (result)
            {
                res.Code = 400;
                res.Success = false;
                res.Msg = "该账户已存在，请修改账户！！！";
                return Ok(res);
            }
            Model.Entitys.User entity = _mapper.Map<User>(userAddDto);
            entity.IsDelete = false;
            await _userServices.AddEntityAsync(entity);
            res.Code = 201;
            res.Data = _mapper.Map<UserDto>(entity);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageModel<string>>> DeleteUser(int id)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _userServices.ExistEntityAsync(u => u.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的用户编号";
                res.Success = false;
                return Ok(res);
            }
            Model.Entitys.User entity = await _userServices.GetEntityByIdAsync(id);
            await _userServices.DeleteEntityAsync(entity);
            res.Msg = "删除成功";
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<MessageModel<UserDto>>> EditUser(UserEditDto userEditDto)
        {

            MessageModel<UserDto> res = new MessageModel<UserDto>();
            bool result = await _userServices.ExistEntityAsync(u => u.Id == userEditDto.Id);
            if (!result)
            {
                res.Code = 404;
                res.Msg = "请输入正确的用户编号";
                res.Success = false;
                return Ok(res);
            }
            Model.Entitys.User entity = _mapper.Map<User>(userEditDto);
            await _userServices.EditEntityAsync(entity);
            res.Data = _mapper.Map<UserDto>(entity);
            return Ok(res);
        }

        private string CreateLink(PagedType pagedType, UserParams userParams)
        {
            switch (pagedType)
            {
                case PagedType.Previous:
                    return Url.Link(nameof(GetUser), new
                    {
                        PageNum = userParams.PageNum - 1,
                        PageSize = userParams.PageSize
                    });

                case PagedType.Next:
                    return Url.Link(nameof(GetUser), new
                    {
                        PageNum = userParams.PageNum + 1,
                        PageSize = userParams.PageSize
                    });
            }
            return string.Empty;
        }
    }
}