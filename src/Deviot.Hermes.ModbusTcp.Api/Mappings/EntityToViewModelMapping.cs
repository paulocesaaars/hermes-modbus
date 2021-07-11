using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            AllowNullCollections = true;

            CreateMap<Token, TokenViewModel>();

            CreateMap<UserInfo, UserInfoViewModel>();
        }
    }
}
