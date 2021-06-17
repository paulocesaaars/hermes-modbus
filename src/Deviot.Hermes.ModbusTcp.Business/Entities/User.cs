using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Base;
using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class User : EntityBase
    {
        public string Name { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public bool Enabled { get; private set; }

        public User()
        {

        }

        public User(Guid id, string name, string userName, string password, bool enabled = false)
        {
            Id = id;
            Name = name;
            UserName = userName.ToLower();
            Password = password;
            Enabled = enabled;
        }

        public void SetName(string value) => Name = value;

        public void SetUserName(string value) => UserName = value;

        public void SetPassword(string value) => Password = Utils.Encript(value);

        public void Enable() => Enabled = true;

        public void Disable() => Enabled = false;
    }
}
