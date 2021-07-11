using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using System;

namespace Deviot.Hermes.ModbusTcp.BDD.Fakes
{
    public static class UserPasswordFake
    {
        public static UserPasswordViewModel GetUserAdmin()
        {
            return new UserPasswordViewModel
            {
                Id = new Guid("7011423f65144a2fb1d798dec19cf466"),
                Password = "admin",
                NewPassword = "12345678"
            };
        }

        public static UserPasswordViewModel GetUserPaulo()
        {
            return new UserPasswordViewModel
            {
                Id = new Guid("e1386eb28ae443e9a13855ce66ebce7d"),
                Password = "123456",
                NewPassword = "12345678"
            };
        }
    }
}
