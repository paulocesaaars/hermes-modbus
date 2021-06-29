using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.ModelViews;
using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.Api.Mappings
{
    public class EntityToModelViewMapping : Profile
    {
        public EntityToModelViewMapping()
        {
            AllowNullCollections = true;

            CreateMap<Token, TokenModelView>();

            CreateMap<User, UserModelView>();
            CreateMap<UserInfo, UserInfoModelView>();
        }
    }
}
