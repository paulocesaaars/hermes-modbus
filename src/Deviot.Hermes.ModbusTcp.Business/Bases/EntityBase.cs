using System;

namespace Deviot.Hermes.ModbusTcp.Business.Base
{
    public class EntityBase
    {
        public Guid Id { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
