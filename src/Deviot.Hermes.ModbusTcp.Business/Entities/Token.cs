using Deviot.Hermes.ModbusTcp.Business.Base;
using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class Token : EntityBase
    {
        public string AccessToken { get; protected set; }

        public UserInfo User { get; protected set; }

        public Token()
        {

        }

        public Token(Guid id, string accessToken, UserInfo user)
        {
            Id = id;
            AccessToken = accessToken;
            User = user;
        }
    }
}
