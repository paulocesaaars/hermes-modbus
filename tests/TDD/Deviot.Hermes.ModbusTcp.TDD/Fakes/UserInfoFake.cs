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

        public static UserInfo GetUserPaulo()
        {
            return new UserInfo(new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                                     "Paulo Cesar de Souza",
                                     "paulo",
                                     true,
                                     false);
        }

        public static UserInfo GetUserBruna()
        {
            return new UserInfo(new Guid("630994d9e6c34d5cb823569560697d67"),
                                     "Bruna Stefano Marques",
                                     "bruna",
                                     true,
                                     false);
        }

        public static UserInfo GetUserPaula()
        {
            return new UserInfo(new Guid("f22e81455a6f4961922a516c54d33dba"),
                                     "Paula Stefano Souza",
                                     "paula",
                                     true,
                                     false);
        }

        public static IEnumerable<UserInfo> GetUsers()
        {
            var users = new List<UserInfo>(2);
            users.Add(GetUserAdmin());
            users.Add(GetUserPaulo());
            users.Add(GetUserBruna());

            return users;
        }
    }
}
