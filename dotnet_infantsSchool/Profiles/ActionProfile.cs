using AutoMapper;
using Model.Dtos;

namespace dotnet_infantsSchool.Profiles
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Model.Entitys.Action, MenuDto>();
            CreateMap<Model.Entitys.Action, ActionDto>().ForMember(dto => dto.Level, ops => ops.MapFrom(entity => entity.ActionTypeId));
            CreateMap<ActionAddDto, Model.Entitys.Action>().ForMember(dto => dto.ActionTypeId, ops => ops.MapFrom(entity => entity.Level));
            CreateMap<ActionDto, Model.Entitys.Action>().ForMember(dto => dto.ActionTypeId, ops => ops.MapFrom(entity => entity.Level));
            CreateMap<Model.Entitys.Action, ActionTreeDto>();
        }
    }
}