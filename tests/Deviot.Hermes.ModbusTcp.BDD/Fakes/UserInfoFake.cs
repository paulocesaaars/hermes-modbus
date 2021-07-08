using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using System;

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
    }
}
