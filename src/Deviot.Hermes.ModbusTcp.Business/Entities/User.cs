using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class User : UserInfo
    {
        public string Password { get; protected set; }

        public User()
        {

        }

        public User(Guid id, string name, string userName, string password, bool enabled = false, bool administrator = false)
        {
            Id = id;
            FullName = name;
            UserName = userName.ToLower();
            Password = password;
            Enabled = enabled;
            Administrator = administrator;
        }

        public void SetPassword(string value) => Password = value;
    }
}
