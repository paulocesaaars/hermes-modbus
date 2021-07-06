using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(ViewModelMappingCollection))]
    public class ViewModelMappingCollection : ICollectionFixture<ViewModelMappingFixture>
    {
    }

    public class ViewModelMappingFixture : ServiceFixtureBase, IDisposable
    {
        public LoginViewModel GetLogin()
        {
            var user = UserFake.GetUserAdmin();
            return new LoginViewModel { UserName = user.UserName, Password = user.Password };
        }
        public UserViewModel GetUser()
        {
            var user = UserFake.GetUserAdmin();
            return new UserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public UserInfoViewModel GetUserInfo()
        {
            var user = UserFake.GetUserAdmin();
            return new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public UserPasswordViewModel GetUserPassword()
        {
            return new UserPasswordViewModel { Password = "12345", NewPassword = "123456" };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
