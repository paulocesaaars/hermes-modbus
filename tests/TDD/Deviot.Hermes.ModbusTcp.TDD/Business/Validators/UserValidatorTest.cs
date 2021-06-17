using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using FluentValidation.TestHelper;
using System;
using System.Linq;
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
        public void User_Name_ValidateMinimumLength()
        {
            var name = GetGenericString(2);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome completo")]
        public void User_UserName_MaximumLength()
        {
            var name = GetGenericString(151);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome de usuário")]
        public void User_UserName_ValidateMinimumLength()
        {
            var name = GetGenericString(2);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome de usuário")]
        public void User_Name_MaximumLength()
        {
            var name = GetGenericString(21);
            var user = GetUser(name);

            var result = _userValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        private static User GetUser(string name = "Teste", string userName = "teste", string password = "123456789")
        {
            return new User(Guid.NewGuid(), name, userName, password, true);
        }

        private static string GetGenericString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
