using AutoMapper;
using Model.Dtos;
using Model.Entitys;

namespace dotnet_infantsSchool.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserAddDto, User>();
            CreateMap<UserEditDto, User>();
            CreateMap<User, TeacherDto>();
        }
    }
}