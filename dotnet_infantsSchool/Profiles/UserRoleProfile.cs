using AutoMapper;
using Model.Dtos;
using Model.Entitys;

namespace dotnet_infantsSchool.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleAddDto, UserRole>();
            CreateMap<UserRoleDto, UserRole>();
        }
    }
}