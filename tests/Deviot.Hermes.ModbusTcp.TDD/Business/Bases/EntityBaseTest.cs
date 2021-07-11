using Deviot.Hermes.ModbusTcp.Business.Base;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Bases
{
    public class EntityBaseTest
    {
        [Fact(DisplayName = "Construtor vazio da classe")]
        public void ValidateConstructor()
        {
            var entity = new EntityBase();

            entity.Id.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Construtor com parametros da classe")]
        public void ValidateConstructorWithParams()
        {
            var id = Guid.NewGuid();
            var entity = new EntityBase(id);

            entity.Id.Should().Be(id);
        }
    }
}
