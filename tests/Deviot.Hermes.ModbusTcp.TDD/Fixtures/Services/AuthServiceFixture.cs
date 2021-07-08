using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Services
{
    public class AuthServiceFixture : ServiceFixtureBase, IDisposable
    {
        public AuthService GetService()
        {
            var loginValidator = new LoginValidator();
            var tokenService = TokenHelper.GetTokenService();
            return new AuthService(_notifier, 
                                   GetLogger<AuthService>(), 
                                   _repository, 
                                   tokenService, 
                                   loginValidator);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
