using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System.Collections.Generic;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    public static class UserInfoFake
    {
        public static UserInfo GetUserAdmin() => UserFake.GetUserAdmin();

        public static UserInfo GetUserPaulo() => UserFake.GetUserPaulo();

        public static UserInfo GetUserBruna() => UserFake.GetUserBruna();

        public static UserInfo GetUserPaula() => UserFake.GetUserPaula();

        public static IEnumerable<UserInfo> GetUsers()
        {
            return new List<UserInfo>(4)
            {
                GetUserAdmin(),
                GetUserPaulo(),
                GetUserBruna(),
                GetUserPaula()
            };
        }

        public static UserInfoViewModel GetUserAdminViewModel()
        {
            var user = GetUserAdmin();
            return new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserInfoViewModel GetUserPauloViewModel()
        {
            var user = GetUserPaulo();
            return new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserInfoViewModel GetUserBrunaViewModel()
        {
            var user = GetUserBruna();
            return new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserInfoViewModel GetUserPaulaViewModel()
        {
            var user = GetUserPaula();
            return new UserInfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static IEnumerable<UserInfoViewModel> GetUsersViewModel()
        {
            return new List<UserInfoViewModel>(4)
            {
                GetUserAdminViewModel(),
                GetUserPauloViewModel(),
                GetUserBrunaViewModel(),
                GetUserPaulaViewModel()
            };
        }
    }
}
