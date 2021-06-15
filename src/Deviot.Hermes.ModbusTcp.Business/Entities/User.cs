using Deviot.Common;
using System;

namespace Deviot.Hermes.ModbusTcp.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public User()
        {

        }

        public User(Guid id, string name, string userName, string password)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
        }
    }
}
