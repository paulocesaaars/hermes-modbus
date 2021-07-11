using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using System;
using System.Collections.Generic;

namespace Deviot.Hermes.ModbusTcp.BDD.Fakes
{
    public static class UserInfoFake
    {
        public static UserInfoViewModel GetUserAdmin()
        {
            return new UserInfoViewModel
            {
                Id = new Guid("7011423f65144a2fb1d798dec19cf466"),
                FullName = "Administrador",
                UserName = "admin",
                Administrator = true,
                Enabled = true
            };
        }

        public static UserInfoViewModel GetUserPaulo()
        {
            return new UserInfoViewModel
            {
                Id = new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                FullName = "Paulo Cesar de Souza",
                UserName = "paulo",
                Administrator = false,
                Enabled = true
            };
        }

        public static UserInfoViewModel GetUserBruna()
        {
            return new UserInfoViewModel
            {
                Id = new Guid("630994d9e6c34d5cb823569560697d67"),
                FullName = "Bruna Stefano Marques",
                UserName = "bruna",
                Administrator = false,
                Enabled = true
            };
        }

        public static UserInfoViewModel GetUserPaula()
        {
            return new UserInfoViewModel
            {
                Id = new Guid("f22e81455a6f4961922a516c54d33dba"),
                FullName = "Paula Stefano Souza",
                UserName = "paula",
                Administrator = false,
                Enabled = true
            };
        }

        public static IEnumerable<UserInfoViewModel> GetUsers()
        {
            var users = new List<UserInfoViewModel>(4)
            {
                GetUserAdmin(),
                GetUserPaulo(),
                GetUserBruna(),
                GetUserPaula()
            };

            return users;
        }
    }
}
