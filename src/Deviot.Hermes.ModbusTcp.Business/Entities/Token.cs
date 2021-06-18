using Deviot.Hermes.ModbusTcp.Business.Base;
using System;

namespace Deviot.Hermes.ModbusTcp.Business.Entities
{
    public class Token : EntityBase
    {
        public string AccessToken { get; set; }

        public Token()
        {

        }

        public Token(Guid id, string accessToken)
        {
            Id = id;
            AccessToken = accessToken;
        }
    }
}
