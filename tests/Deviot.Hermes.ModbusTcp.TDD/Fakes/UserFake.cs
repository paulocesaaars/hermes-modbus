using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    [ExcludeFromCodeCoverage]
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
            var users = new List<User>(2);
            users.Add(GetUserAdmin(passwordEncript));
            users.Add(GetUserPaulo(passwordEncript));
            users.Add(GetUserBruna(passwordEncript));

            return users;
        }
    }
}
