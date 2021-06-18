using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {

        }

        public Task<Token> LoginAsync(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
