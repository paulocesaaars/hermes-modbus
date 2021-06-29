using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using FluentValidation.TestHelper;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Validators
{
    [ExcludeFromCodeCoverage]
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
            var name = Utils.GenerateRandomString(2);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome completo")]
        public void UserName_MaximumLength()
        {
            var name = Utils.GenerateRandomString(151);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome de usuário")]
        public void UserName_ValidateMinimumLength()
        {
            var userName = Utils.GenerateRandomString(2);
            var user = GetUser(userName: userName);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome de usuário")]
        public void Name_MaximumLength()
        {
            var userName = Utils.GenerateRandomString(21);
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
