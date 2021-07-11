using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Api.ViewModels.Bases;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    public class UserControllerTest : ControllerTestBase
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userService;

        public UserControllerTest()
        {
            _userController = _mocker.CreateInstance<UserController>();
            _userService = _mocker.GetMock<IUserService>();
        }

        [Fact]
        public async Task GetAsync_Return200()
        {
            var output = UserInfoFake.GetUserAdminViewModel();
            _userService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(UserInfoFake.GetUserAdmin());

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
            var output = UserInfoFake.GetUsersViewModel();
            _userService.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(UserInfoFake.GetUsers());

            var response = await _userController.GetAllAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async Task GetAllAsync_Return500()
        {
            _userService.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(null as IEnumerable<User>)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.GetAllAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.GetAllAsync())
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

        [Fact]
        public async Task PutAsync_Return200()
        {
            _userService.Setup(x => x.UpdateAsync(It.IsAny<UserInfo>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.OK, "Usuário atualizado com sucesso."));

            var response = await _userController.PutAsync(new UserInfoViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PutAsync_Return403()
        {
            _userService.Setup(x => x.UpdateAsync(It.IsAny<UserInfo>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.Forbidden, "Usuário inválido."));

            var response = await _userController.PutAsync(new UserInfoViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.Forbidden);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PutAsync_Return404()
        {
            _userService.Setup(x => x.UpdateAsync(It.IsAny<UserInfo>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.NotFound, "Usuário não encontrado."));

            var response = await _userController.PutAsync(new UserInfoViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.NotFound);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PutAsync_Return500()
        {
            _userService.Setup(x => x.UpdateAsync(It.IsAny<UserInfo>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.PutAsync(new UserInfoViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task PutAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.UpdateAsync(It.IsAny<UserInfo>()))
                        .Throws(new Exception());

            var response = await _userController.PutAsync(new UserInfoViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Return200()
        {
            _userService.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.OK, "Usuário deletado com sucesso."));

            var response = await _userController.DeleteAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Return404()
        {
            _userService.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.NotFound, "Usuário não encontrado."));

            var response = await _userController.DeleteAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.NotFound);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Return500()
        {
            _userService.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.DeleteAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                        .Throws(new Exception());

            var response = await _userController.DeleteAsync(Guid.NewGuid());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task ChangePasswordAsync_Return200()
        {
            _userService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserPassword>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.OK, "Senha de usuário alterada com sucesso."));

            var response = await _userController.ChangePasswordAsync(new UserPasswordViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task ChangePasswordAsync_Return403()
        {
            _userService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserPassword>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.Forbidden, "Senha inválida."));

            var response = await _userController.ChangePasswordAsync(new UserPasswordViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.Forbidden);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task ChangePasswordAsync_Return500()
        {
            _userService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserPassword>()))
                        .Returns(Task.CompletedTask)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.ChangePasswordAsync(new UserPasswordViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserPassword>()))
                        .Throws(new Exception());

            var response = await _userController.ChangePasswordAsync(new UserPasswordViewModel());
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task CheckUserNameExistAsync_Return200()
        {
            _userService.Setup(x => x.CheckUserNameExistAsync(It.IsAny<string>()))
                        .ReturnsAsync(true);

            var response = await _userController.CheckUserNameExistAsync(string.Empty);
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task CheckUserNameExistAsync_Return500()
        {
            _userService.Setup(x => x.CheckUserNameExistAsync(It.IsAny<string>()))
                        .ReturnsAsync(false)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.CheckUserNameExistAsync(string.Empty);
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task CheckUserNameExistAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.CheckUserNameExistAsync(It.IsAny<string>()))
                        .Throws(new Exception());

            var response = await _userController.CheckUserNameExistAsync(string.Empty);
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task TotalRegistersAsync_Return200()
        {
            _userService.Setup(x => x.TotalRegistersAsync())
                        .ReturnsAsync(20);

            var response = await _userController.TotalRegistersAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.OK);
            result.Data.Should().Equals(20);
        }

        [Fact]
        public async Task TotalRegistersAsync_Return500()
        {
            _userService.Setup(x => x.TotalRegistersAsync())
                        .ReturnsAsync(-1)
                        .Callback(() => _notifier.Notify(HttpStatusCode.InternalServerError, "Banco de dados inacessível."));

            var response = await _userController.TotalRegistersAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task TotalRegistersAsync_ReturnGenericError()
        {
            _userService.Setup(x => x.TotalRegistersAsync())
                        .Throws(new Exception());

            var response = await _userController.TotalRegistersAsync();
            var result = GetGenericActionResult(response);
            var statusCode = GetHttpStatusCode(response);

            statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            result.Messages.FirstOrDefault().Should().BeEquivalentTo(INTERNAL_ERROR_MESSAGE);
            result.Data.Should().BeNull();
        }
    }
}
