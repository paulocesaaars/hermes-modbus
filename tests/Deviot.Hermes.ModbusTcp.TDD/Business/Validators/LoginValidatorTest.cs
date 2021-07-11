using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Validators
{
    public class LoginValidatorTest
    {
        private readonly LoginValidator _loginValidator;

        public LoginValidatorTest()
        {
            _loginValidator = new LoginValidator();
        }

        private static Login GetLogin(string userName = "teste", string password = "123456789")
        {
            return new Login(userName, password);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade nome de usuário")]
        public void UserName_ValidateMinimumLength()
        {
            var userName = Utils.GenerateRandomString(4);
            var login = GetLogin(userName: userName);

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade nome de usuário")]
        public void Name_MaximumLength()
        {
            var userName = Utils.GenerateRandomString(21);
            var login = GetLogin(userName: userName);

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Somente alfanúmerico ou underline propriedade nome de usuário")]
        public void Name_Custom()
        {
            var login = GetLogin(userName: "nome espaço");

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact(DisplayName = "Tamanho mínimo propriedade senha de usuário")]
        public void Password_ValidateMinimumLength()
        {
            var password = Utils.GenerateRandomString(4);
            var login = GetLogin(password: password);

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact(DisplayName = "Tamanho máximo propriedade senha de usuário")]
        public void Password_MaximumLength()
        {
            var password = Utils.GenerateRandomString(21);
            var login = GetLogin(password: password);

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact(DisplayName = "Somente alfanúmerico propriedade senha de usuário")]
        public void Password_Custom()
        {
            var login = GetLogin(password: "senha espaço");

            var result = _loginValidator.TestValidate(login);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
