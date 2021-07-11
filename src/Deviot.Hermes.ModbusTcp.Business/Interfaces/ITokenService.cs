using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface ITokenService
    {
        Token GenerateToken(UserInfo user);
    }
}
