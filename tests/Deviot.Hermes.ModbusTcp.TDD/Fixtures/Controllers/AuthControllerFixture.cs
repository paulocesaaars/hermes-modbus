using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(AuthControllerCollection))]
    public class AuthControllerCollection : ICollectionFixture<AuthControllerFixture>
    {
    }

    public class AuthControllerFixture : ServiceFixtureBase, IDisposable
    {
        private readonly Token _token;

        public AuthControllerFixture()
        {
            _token = TokenHelper.GetToken(UserFake.GetUserAdmin());
        }

        public LoginViewModel GetLoginViewModel()
        {
            var userAdmin = UserFake.GetUserAdmin();
            return new LoginViewModel { UserName = userAdmin.UserName, Password = userAdmin.Password };
        }
            

        public LoginViewModel GetInvalidLoginViewModel() => new LoginViewModel { UserName = "admin", Password = "123" };

        public Token GetToken() => _token;

        public TokenViewModel GetTokenViewModel() 
        {
            var user = UserFake.GetUserAdmin();
            var userInfoViewModel = new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Administrator = user.Administrator,
                Enabled = user.Enabled
            };

            return new TokenViewModel { AccessToken = _token.AccessToken, User = userInfoViewModel };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
