using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ModelViews;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class ModelViewToEntityMapping : Profile
    {
        public ModelViewToEntityMapping()
        {
            AllowNullCollections = true;

            CreateMap<LoginModelView, Login>();
            CreateMap<UserInfoModelView, UserInfo>();
            CreateMap<UserModelView, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
