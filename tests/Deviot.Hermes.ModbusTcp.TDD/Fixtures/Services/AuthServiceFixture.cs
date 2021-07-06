using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Services
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(AuthServiceCollection))]
    public class AuthServiceCollection : ICollectionFixture<AuthServiceFixture>
    {
    }

    public class AuthServiceFixture : ServiceFixtureBase, IDisposable
    {
        public AuthServiceFixture()
        {

        }

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

        public Login GetLoginAdmin()
        {
            var userAdmin = UserFake.GetUserAdmin();
            return new Login(userAdmin.UserName, userAdmin.Password);
        }
            

        public Login GetInvalidLogin() => new Login("invalido", "123");

        public UserInfo GetUserInfoAdmin() => UserFake.GetUserAdmin();
        

        public Token GetTokenAdmin() => TokenHelper.GetToken(GetUserInfoAdmin());

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
