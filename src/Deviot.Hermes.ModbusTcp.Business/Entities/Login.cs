using Deviot.Hermes.ModbusTcp.Business.Base;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class Login : EntityBase
    {
        public string UserName { get; protected set; }

        public string Password { get; protected set; }

        public Login()
        {

        }

        public Login(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
