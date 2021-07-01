using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(AuthServiceCollection))]
    public class AuthServiceCollection : ICollectionFixture<AuthServiceFixture>
    {
    }

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

        public Login GetLoginAdmin() => new Login("admin", "admin");

        public Login GetInvalidLogin() => new Login("invalido", "123");

        public UserInfo GetUserInfoAdmin() => UserInfoFake.GetUserAdmin();

        public Token GetTokenAdmin() => TokenHelper.GetToken(GetUserInfoAdmin());

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
