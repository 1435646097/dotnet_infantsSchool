using AutoMapper;
using Model.Dtos;
using Model.Entitys;

namespace dotnet_infantsSchool.Profiles
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeDto>()
                .ForMember(dto => dto.TeacherName, option => option.MapFrom(entity => entity.User.Name))
                .ForMember(dto => dto.Phone, option => option.MapFrom(entity => entity.User.Phone));
            CreateMap<GradeAddDto, Grade>();
            CreateMap<Grade, GradeAddDto>();
            CreateMap<GradeEditDto, Grade>();
        }
    }
}