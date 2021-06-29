using Deviot.Hermes.ModbusTcp.Business.Base;
using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Bases
{
    [ExcludeFromCodeCoverage]
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
