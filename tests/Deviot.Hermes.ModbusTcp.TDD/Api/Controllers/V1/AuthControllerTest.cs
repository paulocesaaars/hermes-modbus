using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Collections;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    public class AuthControllerTest : ControllerTestBase
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authService;

        public AuthControllerTest()
        {
            _authController = _mocker.CreateInstance<AuthController>();
            _authService = _mocker.GetMock<IAuthService>();
        }

        public TokenViewModel GetTokenViewModel(Token token)
        {
            var userInfoViewModel = new UserInfoViewModel
            {
                Id = token.User.Id,
                FullName = token.User.FullName,
                UserName = token.User.UserName,
                Administrator = token.User.Administrator,
                Enabled = token.User.Enabled
            };

            return new TokenViewModel { AccessToken = token.AccessToken, User = userInfoViewModel };
        }

        [Fact]
        public async Task LoginAsync_Return200()
        {
            var token = TokenHelper.GetToken(UserFake.GetUserAdmin());
            var output = GetTokenViewModel(token);
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(token);

            var response = await _authController.LoginAsync(new LoginViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async Task LoginAsync_Return403()
        {
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(null as Token)
                        .Callback(() => _notifier.Notify(HttpStatusCode.Forbidden, "Senha inválida."));

            var response = await _authController.LoginAsync(new LoginViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.Forbidden);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_Return404()
        {
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(null as Token)
                        .Callback(() => _notifier.Notify(HttpStatusCode.NotFound, "Token não encontrado."));

            var response = await _authController.LoginAsync(new LoginViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.NotFound);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_Return500()
        {
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(null as Token)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _authController.LoginAsync(new LoginViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_ReturnGenericError()
        {
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .Throws(new Exception());

            var response = await _authController.LoginAsync(new LoginViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }
    }
}
