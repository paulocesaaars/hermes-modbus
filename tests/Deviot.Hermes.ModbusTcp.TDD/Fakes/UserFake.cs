using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;
using System.Collections.Generic;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    public static class UserFake
    {
        public static User GetUserAdmin(bool passwordEncript = false)
        {
            var password = "admin";
            return new User(new Guid("7011423f65144a2fb1d798dec19cf466"),
                                     "Administrador",
                                     "admin",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     true);
        }

        public static User GetUserPaulo(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                                     "Paulo Cesar de Souza",
                                     "paulo",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public static User GetUserBruna(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("630994d9e6c34d5cb823569560697d67"),
                                     "Bruna Stefano Marques",
                                     "bruna",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public static User GetUserPaula(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("f22e81455a6f4961922a516c54d33dba"),
                                     "Paula Stefano Souza",
                                     "paula",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public static IEnumerable<User> GetUsers(bool passwordEncript = false)
        {
            return new List<User>(4)
            {
                GetUserAdmin(passwordEncript),
                GetUserPaulo(passwordEncript),
                GetUserBruna(passwordEncript),
                GetUserPaula(passwordEncript)
            };
        }

        public static UserViewModel GetUserAdminViewModel(bool passwordEncript = false)
        {
            var user = GetUserAdmin(passwordEncript);
            return new UserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserViewModel GetUserPauloViewModel(bool passwordEncript = false)
        {
            var user = GetUserPaulo(passwordEncript);
            return new UserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserViewModel GetUserBrunaViewModel(bool passwordEncript = false)
        {
            var user = GetUserBruna(passwordEncript);
            return new UserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static UserViewModel GetUserPaulaViewModel(bool passwordEncript = false)
        {
            var user = GetUserPaula(passwordEncript);
            return new UserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Enabled = user.Enabled,
                Administrator = user.Administrator
            };
        }

        public static IEnumerable<UserViewModel> GetUsersViewModel(bool passwordEncript = false)
        {
            return new List<UserViewModel>(4)
            {
                GetUserAdminViewModel(passwordEncript),
                GetUserPauloViewModel(passwordEncript),
                GetUserBrunaViewModel(passwordEncript),
                GetUserPaulaViewModel(passwordEncript)
            };
        }
    }
}
