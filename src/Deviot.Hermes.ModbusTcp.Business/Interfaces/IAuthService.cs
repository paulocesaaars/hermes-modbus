using Deviot.Hermes.ModbusTcp.Business.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }

        UserInfo GetLoggedUser();

        void SetLoggedUser(UserInfo user);

        Task<Token> LoginAsync(Login login);
    }
}
