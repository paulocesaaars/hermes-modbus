using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    public class AuthControllerTest : ControllerTestBase
    {
        AuthController _authController;
        Mock<IAuthService> _authService;

        public AuthControllerTest()
        {
            _authController = Mocker.CreateInstance<AuthController>();
            _authService = Mocker.GetMock<IAuthService>();
        }

        private Token GetToken()
        {
            var user = UserFake.GetUserAdmin();
            return TokenHelper.GetToken(user);
        }

        [Fact]
        public async Task LoginAsync_ReturnOk()
        {
            var token = GetToken();
            var loginViewModel = new LoginViewModel { UserName = "admin", Password = "admin" };
            _authService.Setup(x => x.LoginAsync(It.IsAny<Login>()))
                        .ReturnsAsync(token);

            var response = await _authController.LoginAsync(loginViewModel);
            var contentResult = response.Result as ContentResult;
            var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(contentResult.Content);

            contentResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
