using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            AllowNullCollections = true;

            CreateMap<LoginViewModel, Login>();
            CreateMap<UserInfoViewModel, UserInfo>();
            CreateMap<UserPasswordViewModel, UserPassword>();
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
