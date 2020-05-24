using AutoMapper;
using Model.Dtos;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Profiles
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleAddDto, UserRole>();
            CreateMap<UserRoleDto, UserRole>();
        }
    }
}
