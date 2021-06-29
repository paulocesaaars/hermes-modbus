using Deviot.Hermes.ModbusTcp.Business.Base;
using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class UserInfo : EntityBase
    {
        public string FullName { get; protected set; }

        public string UserName { get; protected set; }

        public bool Enabled { get; protected set; }

        public bool Administrator { get; protected set; }

        public UserInfo()
        {

        }

        public UserInfo(Guid id, string fullName, string userName, bool enabled, bool administrator)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Enabled = enabled;
            Administrator = administrator;
        }

        public void SetFullName(string value) => FullName = value;

        public void SetUserName(string value) => UserName = value.ToLower();

        public void SetEnabled(bool value) => Enabled = value;

        public void SetAdministrator(bool value) => Administrator = value;
    }
}
