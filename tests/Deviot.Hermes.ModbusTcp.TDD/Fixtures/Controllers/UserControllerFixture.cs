using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(UserControllerCollection))]
    public class UserControllerCollection : ICollectionFixture<UserControllerFixture>
    {
    }

    public class UserControllerFixture : ServiceFixtureBase, IDisposable
    {
        public User GetUserAdmin() => UserFake.GetUserAdmin();

        public User GetUserPaulo() => UserFake.GetUserPaulo();

        public IEnumerable<UserInfo> GetUsers()
        {
            var users = new List<UserInfo>(2)
            {
                UserFake.GetUserAdmin(),
                UserFake.GetUserPaulo()
            };

            return users;
        }

        public UserInfoViewModel GetUserInfoAdminViewModel()
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

            return userInfoViewModel;
        }

        public UserInfoViewModel GetUserInfoPauloViewModel()
        {
            var user = UserFake.GetUserPaulo();
            var userInfoViewModel = new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Administrator = user.Administrator,
                Enabled = user.Enabled
            };

            return userInfoViewModel;
        }

        public IEnumerable<UserInfoViewModel> GetUserInfoViewModels()
        {
            var users = new List<UserInfoViewModel>(2)
            {
                GetUserInfoAdminViewModel(),
                GetUserInfoPauloViewModel()
            };

            return users;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
