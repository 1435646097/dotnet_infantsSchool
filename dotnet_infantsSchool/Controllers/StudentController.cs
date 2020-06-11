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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize("actionAuthrization")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly IUserServices _userServices;
        private readonly IGradeServices _gradeServices;
        private readonly IMapper _mapper;

        public StudentController(IStudentServices studentServices, IUserServices userServices, IGradeServices gradeServices, IMapper mapper)
        {
            _studentServices = studentServices;
            _userServices = userServices;
            _gradeServices = gradeServices;
            _mapper = mapper;
        }
        [HttpGet(Name = nameof(GetStudents))]
        public async Task<ActionResult<MessageModel<IEnumerable<StudentDto>>>> GetStudents([FromQuery] StudentParams studentParams)
        {
            MessageModel<IEnumerable<StudentDto>> res = new MessageModel<IEnumerable<StudentDto>>();
            PagedList<Student> list = await _studentServices.GetStudentPaged(studentParams);
            //配置x-pagination响应头
            string previousLink = list.HasPrevious ? CreateLink(PagedType.Previous, studentParams) : null;
            string nextLink = list.HasNext ? CreateLink(PagedType.Next, studentParams) : null;
            var pagination = new
            {
                currentPage = list.PageNum,
                totalPage = list.TotalPage,
                totalCount = list.TotalCount,
                previousLink,
                nextLink
            };
            HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
            res.Data = _mapper.Map<IEnumerable<StudentDto>>(list);
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<MessageModel<IEnumerable<GradeDto>>>> GetGradesInfo()
        {
            MessageModel<IEnumerable<GradeDto>> res = new MessageModel<IEnumerable<GradeDto>>();
            int uId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "id").FirstOrDefault().Value);
            User entity = await _userServices.GetEntityByIdAsync(uId);
            UserRole userRole = entity.UserRole.Where(u => u.RoleId == 1).FirstOrDefault();
            if (userRole != null)//为管理员
            {
                res.Data = _mapper.Map<IEnumerable<GradeDto>>(await _gradeServices.GetEntitys().ToListAsync());
                return Ok(res);
            }
            //不为管理员，为正常的教师
            res.Data = _mapper.Map<IEnumerable<GradeDto>>(await _gradeServices.GetEntitys().Where(g => g.UserId == uId).ToListAsync());
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageModel<string>>> DeleteStudent(int id)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _studentServices.ExistEntityAsync(s => s.Id == id);
            if (!result)
            {
                res.Msg = "请输入正确的学生Id";
                res.Code = 404;
                res.Success = false;
                return Ok(res);
            }
            Student entity = await _studentServices.GetEntityByIdAsync(id);
            await _studentServices.DeleteEntityAsync(entity);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel<StudentDto>>> GetStudentById(int id)
        {

            MessageModel<StudentDto> res = new MessageModel<StudentDto>();
            bool result = await _studentServices.ExistEntityAsync(s => s.Id == id);
            if (!result)
            {
                res.Msg = "请输入正确的学生Id";
                res.Code = 404;
                res.Success = false;
                return Ok(res);
            }
            Student entity = await _studentServices.GetEntityByIdAsync(id);
            res.Data = _mapper.Map<StudentDto>(entity);
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<MessageModel<StudentDto>>> AddStudent(StudentAddDto studentAddDto)
        {
            MessageModel<StudentDto> res = new MessageModel<StudentDto>();
            Student entity = _mapper.Map<Student>(studentAddDto);
            await _studentServices.AddEntityAsync(entity);
            res.Data = _mapper.Map<StudentDto>(entity);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<MessageModel<string>>> EditStudent(StudentEditDto studentEditDto)
        {
            MessageModel<string> res = new MessageModel<string>();
            Student entity = _mapper.Map<Student>(studentEditDto);
            await _studentServices.EditEntityAsync(entity);
            return Ok(res);
        }
        private string CreateLink(PagedType pagedType, StudentParams studentParams)
        {
            switch (pagedType)
            {
                case PagedType.Previous:
                    return Url.Link(nameof(GetStudents), new
                    {
                        PageNum = studentParams.PageNum - 1,
                        PageSize = studentParams.PageSize,
                        Name = studentParams.Name,
                        GradeId = studentParams.GradeId
                    });

                case PagedType.Next:
                    return Url.Link(nameof(GetStudents), new
                    {
                        PageNum = studentParams.PageNum + 1,
                        PageSize = studentParams.PageSize,
                        Name = studentParams.Name,
                        GradeId = studentParams.GradeId
                    });
            }
            return string.Empty;
        }
    }
}
