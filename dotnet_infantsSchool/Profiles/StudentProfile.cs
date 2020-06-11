using AutoMapper;
using Microsoft.Extensions.Options;
using Model.Dtos;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dto => dto.Gender, options => options.MapFrom(entity => (bool)entity.Gender ? "男" : "女"))
                .ForMember(dto=>dto.GradeName,option=>option.MapFrom(entity=>entity.Grade.Name));
            CreateMap<StudentAddDto, Student>();
            CreateMap<StudentEditDto, Student>();
        }
    }
}
