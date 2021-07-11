using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    public static class LoginFake
    {
        public static Login GetLoginAdmin()
        {
            var user = UserFake.GetUserAdmin();
            return new Login(user.UserName, user.Password);
        }

        public static Login GetLoginPaulo()
        {
            var user = UserFake.GetUserPaulo();
            return new Login(user.UserName, user.Password);
        }

        public static Login GetLoginBruna()
        {
            var user = UserFake.GetUserBruna();
            return new Login(user.UserName, user.Password);
        }

        public static Login GetLoginPaula()
        {
            var user = UserFake.GetUserPaula();
            return new Login(user.UserName, user.Password);
        }

        public static LoginViewModel GetLoginAdminViewModel()
        {
            var login = GetLoginAdmin();
            return new LoginViewModel
            {
                UserName = login.UserName,
                Password = login.Password
            };
        }

        public static LoginViewModel GetLoginPauloViewModel()
        {
            var login = GetLoginPaulo();
            return new LoginViewModel
            {
                UserName = login.UserName,
                Password = login.Password
            };
        }

        public static LoginViewModel GetLoginBrunaViewModel()
        {
            var login = GetLoginBruna();
            return new LoginViewModel
            {
                UserName = login.UserName,
                Password = login.Password
            };
        }

        public static LoginViewModel GetLoginPaulaViewModel()
        {
            var login = GetLoginPaula();
            return new LoginViewModel
            {
                UserName = login.UserName,
                Password = login.Password
            };
        }
    } 
}
