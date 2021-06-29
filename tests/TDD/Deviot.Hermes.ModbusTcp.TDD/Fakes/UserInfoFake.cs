using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    [ExcludeFromCodeCoverage]
    public static class UserInfoFake
    {
        public static UserInfo GetUserAdmin()
        {
            return new UserInfo(new Guid("7011423f65144a2fb1d798dec19cf466"),
                                     "Administrador",
                                     "admin",
                                     true,
                                     true);
        }

        public static UserInfo GetUserNormal()
        {
            return new UserInfo(new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                                     "Paulo Cesar de Souza",
                                     "paulo",
                                     true,
                                     false);
        }

        public static IEnumerable<UserInfo> GetUsers()
        {
            var users = new List<UserInfo>(2);
            users.Add(GetUserAdmin());
            users.Add(GetUserNormal());

            return users;
        }
    }
}
