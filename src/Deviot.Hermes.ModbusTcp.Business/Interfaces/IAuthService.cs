using Deviot.Hermes.ModbusTcp.Business.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string name, string password);
    }
}
