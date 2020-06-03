using AutoMapper;
using Model.Dtos;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
