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
    public class UserPasswordValidatorTest
    {
        private readonly UserPasswordValidator _userPasswordValidator;

        public UserPasswordValidatorTest()
        {
            _userPasswordValidator = new UserPasswordValidator();
        }

        private static UserPassword GetUserPassword(string password = "123456789", string newPassword = "123456789")
        {
            return new UserPassword(Guid.NewGuid(), password, newPassword);
        }


        [Fact(DisplayName = "Tamanho mínimo propriedade senha de usuário")]
        public void Password_ValidateMinimumLength()
        {
            var password = Utils.GenerateRandomString(4);
            var userPassword = GetUserPassword(password: password);

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade senha de usuário")]
        public void Password_MaximumLength()
        {
            var password = Utils.GenerateRandomString(21);
            var userPassword = GetUserPassword(password: password);

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact(DisplayName = "Somente alfanúmerico propriedade senha de usuário")]
        public void Password_Custom()
        {
            var userPassword = GetUserPassword(password: "senha espaço");

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nova senha de usuário")]
        public void NewPassword_ValidateMinimumLength()
        {
            var password = Utils.GenerateRandomString(4);
            var userPassword = GetUserPassword(newPassword: password);

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nova senha de usuário")]
        public void NewPassword_MaximumLength()
        {
            var password = Utils.GenerateRandomString(21);
            var userPassword = GetUserPassword(newPassword: password);

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }

        [Fact(DisplayName = "Somente alfanúmerico propriedade nova senha de usuário")]
        public void NewPassword_Custom()
        {
            var userPassword = GetUserPassword(newPassword: "senha espaço");

            var result = _userPasswordValidator.TestValidate(userPassword);

            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }
    }
}
