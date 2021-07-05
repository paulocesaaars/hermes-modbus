using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class EntityToModelViewMapping : Profile
    {
        public EntityToModelViewMapping()
        {
            AllowNullCollections = true;

            CreateMap<Token, TokenViewModel>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserInfo, UserInfoViewModel>();
        }
    }
}
