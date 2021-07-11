using Deviot.Hermes.ModbusTcp.Business.Base;
using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class UserPassword : EntityBase
    {
        public string Password { get; protected set; }

        public string NewPassword { get; protected set; }

        public UserPassword()
        {

        }

        public UserPassword(Guid id, string password, string newPassword)
        {
            Id = id;
            Password = password;
            NewPassword = newPassword;
        }
    }
}
