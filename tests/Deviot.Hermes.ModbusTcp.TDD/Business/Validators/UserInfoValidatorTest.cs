using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Validators
{
    public class UserInfoValidatorTest
    {
        private readonly UserInfoValidator _userInfoValidator;

        public UserInfoValidatorTest()
        {
            _userInfoValidator = new UserInfoValidator();
        }

        private static UserInfo GetUser(string name = "Teste", string userName = "teste")
        {
            return new UserInfo(Guid.NewGuid(), name, userName, true, true);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome completo")]
        public void Name_ValidateMinimumLength()
        {
            var name = Utils.GenerateRandomString(2);
            var user = GetUser(name);

            var result = _userInfoValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome completo")]
        public void UserName_MaximumLength()
        {
            var name = Utils.GenerateRandomString(151);
            var user = GetUser(name);

            var result = _userInfoValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome de usuário")]
        public void UserName_ValidateMinimumLength()
        {
            var userName = Utils.GenerateRandomString(2);
            var user = GetUser(userName: userName);

            var result = _userInfoValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome de usuário")]
        public void Name_MaximumLength()
        {
            var userName = Utils.GenerateRandomString(21);
            var user = GetUser(userName: userName);

            var result = _userInfoValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Somente alfanúmerico ou underline propriedade nome de usuário")]
        public void Name_Custom()
        {
            var user = GetUser(userName: "nome espaço");

            var result = _userInfoValidator.TestValidate(user);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }
    }
}
