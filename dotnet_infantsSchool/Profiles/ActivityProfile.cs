using AutoMapper;
using Model.Dtos;
using Model.Entitys;

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