using AutoMapper;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    /// <summary>
    /// 活动管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize("actionAuthrization")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityServices _activityServices;
        private readonly IMapper _mapper;

        public ActivityController(IActivityServices activityServices, IMapper mapper)
        {
            _activityServices = activityServices;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取所有活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<ActivityDto>>>> GetActivity()
        {
            MessageModel<IEnumerable<ActivityDto>> res = new MessageModel<IEnumerable<ActivityDto>>();
            List<Activity> list = await _activityServices.GetEntitys().ToListAsync();
            List<ActivityDto> activityDtos = _mapper.Map<List<ActivityDto>>(list);
            for (int i = 0; i < list.Count; i++)
            {
                activityDtos[i].PictureList = list[i].ActivityPicture.Select(a => a.Path).ToList();
                activityDtos[i].OnePicture = activityDtos[i].PictureList.Count() > 0 ? activityDtos[i].PictureList[0] : null;
            }
            res.Data = activityDtos;
            return Ok(res);
        }
        /// <summary>
        /// 根据条件获取活动
        /// </summary>
        /// <param name="activityParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<MessageModel<IEnumerable<ActivityDto>>>> GetActivityByParams([FromQuery] ActivityParams activityParams)
        {
            MessageModel<IEnumerable<ActivityDto>> res = new MessageModel<IEnumerable<ActivityDto>>();
            IQueryable<Activity> temp = _activityServices.GetEntitys();
            if (!string.IsNullOrWhiteSpace(activityParams.name))
            {
                temp = temp.Where(a => a.Name.Contains(activityParams.name) || a.Remark.Contains(activityParams.name));
            }
            if (activityParams.GradeId > 0)
            {
                temp = temp.Where(a => a.GradeId == activityParams.GradeId);
            }
            List<Activity> list = await temp.ToListAsync();
            List<ActivityDto> activityDtos = _mapper.Map<List<ActivityDto>>(list);
            for (int i = 0; i < list.Count; i++)
            {
                activityDtos[i].PictureList = list[i].ActivityPicture.Select(a => a.Path).ToList();
                activityDtos[i].OnePicture = activityDtos[i].PictureList.Count() > 0 ? activityDtos[i].PictureList[0] : null;
            }
            res.Data = activityDtos;
            return Ok(res);
        }
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="activityAddDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MessageModel<ActivityDto>>> AddActivity(ActivityAddDto activityAddDto)
        {
            MessageModel<ActivityDto> res = new MessageModel<ActivityDto>();
            Activity entity = _mapper.Map<Activity>(activityAddDto);
            await _activityServices.AddEntityAsync(entity);
            res.Data = _mapper.Map<ActivityDto>(entity);
            return Ok(res);
        }
    }
}