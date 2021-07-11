using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    public static class UserPasswordFake
    {
        public static UserPassword GetUserAdmin(string newPassword = "12345678")
        {
            var user = UserFake.GetUserAdmin();
            return new UserPassword(user.Id, user.Password, newPassword);
        }

        public static UserPassword GetUserPaulo(string newPassword = "12345678")
        {
            var user = UserFake.GetUserPaulo();
            return new UserPassword(user.Id, user.Password, newPassword);
        }

        public static UserPassword GetUserBruna(string newPassword = "12345678")
        {
            var user = UserFake.GetUserBruna();
            return new UserPassword(user.Id, user.Password, newPassword);
        }

        public static UserPassword GetUserPaula(string newPassword = "12345678")
        {
            var user = UserFake.GetUserPaula();
            return new UserPassword(user.Id, user.Password, newPassword);
        }

        public static UserPasswordViewModel GetPasswordAdminViewModel(string newPassword = "12345678")
        {
            var user = GetUserAdmin(newPassword);
            return new UserPasswordViewModel
            {
                Id = user.Id,
                Password = user.Password,
                NewPassword = user.NewPassword
            };
        }

        public static UserPasswordViewModel GetPasswordPauloViewModel(string newPassword = "12345678")
        {
            var user = GetUserPaulo(newPassword);
            return new UserPasswordViewModel
            {
                Id = user.Id,
                Password = user.Password,
                NewPassword = user.NewPassword
            };
        }

        public static UserPasswordViewModel GetPasswordBrunaViewModel(string newPassword = "12345678")
        {
            var user = GetUserBruna(newPassword);
            return new UserPasswordViewModel
            {
                Id = user.Id,
                Password = user.Password,
                NewPassword = user.NewPassword
            };
        }

        public static UserPasswordViewModel GetPasswordPaulaViewModel(string newPassword = "12345678")
        {
            var user = GetUserPaula(newPassword);
            return new UserPasswordViewModel
            {
                Id = user.Id,
                Password = user.Password,
                NewPassword = user.NewPassword
            };
        }
    }
}
