using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class ModelViewToEntityMapping : Profile
    {
        public ModelViewToEntityMapping()
        {
            AllowNullCollections = true;

            CreateMap<LoginViewModel, Login>();
            CreateMap<UserInfoViewModel, UserInfo>();
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
