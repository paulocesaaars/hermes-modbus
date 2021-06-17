using System;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Business.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
