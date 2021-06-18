using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Extensions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Validators
{
    public class UserValidatorTest
    {
        private readonly UserValidator _userValidator;

        public UserValidatorTest()
        {
            _userValidator = new UserValidator();
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome completo")]
        public void Name_ValidateMinimumLength()
        {
            var name = TestUtils.GetGenericString(2);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome completo")]
        public void UserName_MaximumLength()
        {
            var name = TestUtils.GetGenericString(151);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome de usuário")]
        public void UserName_ValidateMinimumLength()
        {
            var userName = TestUtils.GetGenericString(2);
            var user = GetUser(userName: userName);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome de usuário")]
        public void Name_MaximumLength()
        {
            var userName = TestUtils.GetGenericString(21);
            var user = GetUser(userName: userName);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Somente alfanúmerico propriedade nome de usuário")]
        public void Name_Custom()
        {
            var user = GetUser(userName: "nome espaço");

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        private static User GetUser(string name = "Teste", string userName = "teste", string password = "123456789")
        {
            return new User(Guid.NewGuid(), name, userName, password, true);
        }
    }
}
