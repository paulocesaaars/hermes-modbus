using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Entities
{
    public class UserTest
    {
        [Fact(DisplayName = "Construtor da classe")]
        public void ValidateConstructor()
        {
            var user = new User(Guid.NewGuid(), "Teste", "teste", "123456");

            user.Id.Should().NotBeEmpty();
            user.Name.Should().NotBeEmpty();
            user.UserName.Should().NotBeEmpty();
            user.Password.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Nome de usuário deve ser sempre em letra minúscula")]
        public void Validate_ToLower_Username()
        {
            var user = new User(Guid.NewGuid(), "Teste", "Teste", "123456");

            user.UserName.Should().Be(user.UserName.ToLower());
        }
    }
}
