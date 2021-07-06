using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers;
using FluentAssertions;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(AuthControllerCollection))]
    public class AuthControllerTest : ControllerTestBase
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authService;
        private readonly AuthControllerFixture _authControllerFixture;

        public AuthControllerTest(AuthControllerFixture authControllerFixture)
        {
            _authController = _mocker.CreateInstance<AuthController>();
            _authService = _mocker.GetMock<IAuthService>();
            _authControllerFixture = authControllerFixture;
        }

        [Fact]
        public async Task LoginAsync_Return200()
        {
            var output = _authControllerFixture.GetTokenViewModel();
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(_authControllerFixture.GetToken());

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
