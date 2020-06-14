using AutoMapper;
using Model.Dtos;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityAddDto, Activity>().ForMember(des => des.ActivityPicture, option => option.MapFrom(source => source.Pics));
        }
    }
}
