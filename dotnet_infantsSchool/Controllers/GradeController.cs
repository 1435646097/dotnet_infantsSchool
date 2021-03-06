﻿using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    /// <summary>
    /// 班级管理
    /// </summary>
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
        /// <summary>
        /// 获取所有班级
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<GradeDto>>>> GetGrades()
        {
            MessageModel<IEnumerable<GradeDto>> res = new MessageModel<IEnumerable<GradeDto>>();
            IEnumerable<Grade> entitys = await _gradeServices.GetEntitys().ToListAsync();
            IEnumerable<GradeDto> gradeDtos = _mapper.Map<IEnumerable<GradeDto>>(entitys);
            res.Data = gradeDtos;
            return Ok(res);
        }
        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="gradeAddDto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据id删除班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据id获取班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 修改班级
        /// </summary>
        /// <param name="gradeEditDto"></param>
        /// <returns></returns>

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