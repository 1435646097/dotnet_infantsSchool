using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    [ApiController]
    [Route("api/grade")]
    [Authorize("actionAuthrization")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeServices _gradeServices;
        private readonly IMapper _mapper;

        public GradeController(IGradeServices gradeServices, IMapper mapper)
        {
            _gradeServices = gradeServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<GradeDto>>>> GetGrades()
        {
            MessageModel<IEnumerable<GradeDto>> res = new MessageModel<IEnumerable<GradeDto>>();
            IEnumerable<Grade> entitys = await _gradeServices.GetEntitys().ToListAsync();
            IEnumerable<GradeDto> gradeDtos = _mapper.Map<IEnumerable<GradeDto>>(entitys);
            res.Data = gradeDtos;
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<MessageModel<GradeDto>>> AddGrade(GradeAddDto gradeAddDto)
        {
            MessageModel<GradeDto> res = new MessageModel<GradeDto>();
            Grade entity = _mapper.Map<Grade>(gradeAddDto);
            entity.CreateTime = DateTime.Now;
            await _gradeServices.AddEntityAsync(entity);
            res.Data = _mapper.Map<GradeDto>(entity);
            res.Code = 201;
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageModel<string>>> DeleteGrade(int id)
        {
            MessageModel<string> res = new MessageModel<string>();
            bool result = await _gradeServices.ExistEntityAsync(a => a.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                res.Data = "失败！！！";
                return Ok(res);
            }
            Grade entity = await _gradeServices.GetEntityByIdAsync(id);
            await _gradeServices.DeleteEntityAsync(entity);
            res.Msg = "成功！！！";
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel<GradeDto>>> GetGradeById(int id)
        {
            MessageModel<GradeDto> res = new MessageModel<GradeDto>();
            bool result = await _gradeServices.ExistEntityAsync(a => a.Id == id);
            if (!result)
            {
                res.Code = 404;
                res.Success = false;
                return Ok(res);
            }
            Grade entity = await _gradeServices.GetEntityByIdAsync(id);
            res.Data = _mapper.Map<GradeDto>(entity);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<MessageModel<string>>> EditGrade(GradeEditDto gradeEditDto)
        {
            MessageModel<string> res = new MessageModel<string>();
            Grade entity = _mapper.Map<Grade>(gradeEditDto);
            await _gradeServices.EditEntityAsync(entity);
            res.Msg = "成功！！！";
            return Ok(res);
        }
    }
}
