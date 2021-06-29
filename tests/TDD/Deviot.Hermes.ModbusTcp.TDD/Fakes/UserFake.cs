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

        public static User GetUserNormal(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                                     "Paulo Cesar de Souza",
                                     "paulo",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public static IEnumerable<User> GetUsers(bool passwordEncript = false)
        {
            var users = new List<User>(2);
            users.Add(GetUserAdmin(passwordEncript));
            users.Add(GetUserNormal(passwordEncript));

            return users;
        }
    }
}
