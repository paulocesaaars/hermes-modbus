using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(UserControllerCollection))]
    public class UserControllerTest : ControllerTestBase
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userService;
        private readonly UserControllerFixture _userControllerFixture;

        public UserControllerTest(UserControllerFixture userControllerFixture)
        {
            _userController = _mocker.CreateInstance<UserController>();
            _userService = _mocker.GetMock<IUserService>();
            _userControllerFixture = userControllerFixture;
        }

        [Fact]
        public async Task GetAsync_Return200()
        {
            var output = _userControllerFixture.GetUserInfoAdminViewModel();
            _userService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(_userControllerFixture.GetUserAdmin());

            var response = await _userController.GetAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async Task GetAsync_Return404()
        {
            _userService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(null as User)
                        .Callback(() => _notifier.Notify(HttpStatusCode.NotFound, "Usuário não encontrado."));

            var response = await _userController.GetAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.NotFound);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_Return500()
        {
            _userService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(null as User)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.GetAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                         .Throws(new Exception());

            var response = await _userController.GetAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_Return200()
        {
            var output = _userControllerFixture.GetUserInfoViewModels();
            _userService.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync(_userControllerFixture.GetUsers());

            var response = await _userController.GetAllAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async Task GetAllAsync_Return500()
        {
            _userService.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync(null as IEnumerable<User>)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.GetAllAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                         .Throws(new Exception());

            var response = await _userController.GetAllAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PostAsync_Return201()
        {
            _userService.Setup(x => x.InsertAsync(It.IsAny<User>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.Created, "Usuário inserido com sucesso."));

            var response = await _userController.PostAsync(new UserViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.Created);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PostAsync_Return403()
        {
            _userService.Setup(x => x.InsertAsync(It.IsAny<User>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.Forbidden, "Usuário inválido."));

            var response = await _userController.PostAsync(new UserViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.Forbidden);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PostAsync_Return500()
        {
            _userService.Setup(x => x.InsertAsync(It.IsAny<User>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.PostAsync(new UserViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            _notifier.HasNotifications.Should().BeTrue();
            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PostAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.InsertAsync(It.IsAny<User>()))
                        .Throws(new Exception());

            var response = await _userController.PostAsync(new UserViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }
    }
}
