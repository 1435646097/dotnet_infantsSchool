using AutoMapper;
using Model.Dtos;
using Model.Entitys;

namespace dotnet_infantsSchool.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    }
}