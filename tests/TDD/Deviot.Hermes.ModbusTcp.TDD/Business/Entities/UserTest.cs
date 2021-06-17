using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Entities
{
    public class UserTest
    {
        private static User GetUserTest(Guid id) => new User(id, "Teste", "Teste", "123", true);

        [Fact(DisplayName = "Construtor da classe")]
        public void ValidateConstructor()
        {
            var id = Guid.NewGuid();
            var user = GetUserTest(id);

            user.Id.Should().Be(id);
            user.Name.Should().Be("Teste");
            user.UserName.Should().Be("teste");
            user.Password.Should().Be(Utils.Encript("123"));
            user.Enabled.Should().Be(true);
        }

        [Fact(DisplayName = "Validar metódos sets")]
        public void Validate_ToLower_Username()
        {
            var user = new User();
            var esperado = GetUserTest(user.Id);

            user.SetName("Teste");
            user.SetUserName("Teste");
            user.SetPassword("123");
            user.SetEnabled(true);

            user.Should().BeEquivalentTo(esperado);
        }
    }
}
