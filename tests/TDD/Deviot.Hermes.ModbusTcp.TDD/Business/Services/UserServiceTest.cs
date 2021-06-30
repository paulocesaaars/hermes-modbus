using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Business.Services;
using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(UserServiceCollection))]
    public class UserServiceTest
    {
        private readonly UserServiceFixture _userServiceFixture;

        public UserServiceTest(UserServiceFixture userServiceFixture)
        {
            _userServiceFixture = userServiceFixture;
            _userServiceFixture.ClearNotifications();

        }


        [Fact(DisplayName = "Retorna usuário por id - Sucesso")]
        public async Task GetUserForId_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserInfoAdmin();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var resultado = await userService.GetAsync(user.Id);

            resultado.Should().BeEquivalentTo(user);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Retorna usuário por id - Usuário não encontrado")]
        public async Task GetUserForId_NotFoundAsync()
        {
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var resultado = await userService.GetAsync(Guid.NewGuid());

            resultado.Should().BeNull();
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.NotFound)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Retorna todos usuários - Sucesso")]
        public async Task GetUsers_SuccessAsync()
        {
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var resultado = await userService.GetAllAsync();

            resultado.Should().HaveCountGreaterOrEqualTo(3);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Inserir usuário normal - Sucesso")]
        public async Task InsertNormalUser_SuccessAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario novo", "usuario_novo", "123456");
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);

            result.Should().BeEquivalentTo(newUser);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Created)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Inserir usuário normal - Erro de validação")]
        public async Task InsertNormalUser_ValidationErrorAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario novo", "usuario novo", "123");
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);

            result.Should().BeNull();
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Inserir usuário administrador - Sucesso")]
        public async Task InsertAdminUser_SuccessAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario administrador", "usuario_admin", "123456", true, true);
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);
            await userService.DeleteAsync(newUser.Id);

            result.Should().BeEquivalentTo(newUser);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Created)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Inserir usuário administrador - Erro de autorização")]
        public async Task InsertAdminUser_AuthorizationErrorAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario administrador", "usuario_admin", "123456", true, true);
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);

            result.Should().BeNull();
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Inserir usuário - Nome de usuário já existente")]
        public async Task InsertUser_ExistingUsername()
        {
            var newUser = new User(Guid.NewGuid(), "Paulo Cesar", "paulo", "123");
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);

            result.Should().BeNull();
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Atualizar seu usuário - Sucesso")]
        public async Task UpdateUser_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetFullName("Paulo Souza");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Atualizar seu usuário - Erro de validação")]
        public async Task UpdateUser_ValidationErrorAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetUserName("paulo cesar");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Atualizar seu usuário para administrador - Erro de autorização")]
        public async Task UpdateOtherUserForAdmin_AuthorizationError()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetAdministrator(true);

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Atualizar seu usuário - Nome de usuário já existente")]
        public async Task UpdateUser_ExistingUsernameErrorAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetUserName("bruna");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Atualizar outro usuário - Sucesso")]
        public async Task UpdateOtherUser_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            user.SetFullName("Paulo César");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Atualizar outro usuário - Erro de autorização")]
        public async Task UpdateUserExistingUsername()
        {
            var user = _userServiceFixture.GetUserInfoBruna();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetFullName("Bruna Stefano");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Deletar usuário - Sucesso")]
        public async Task DeleteUser_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaula();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.DeleteAsync(user.Id);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Deletar usuário - Erro de autorização")]
        public async Task DeleteUser_AuthorizationErrorAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaula();
            var loggedUser = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetService(loggedUser);

            await userService.DeleteAsync(user.Id);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Deletar usuário - Usuário não encontrado")]
        public async Task DeleteUser_UserNotFoundAsync()
        {
            var id = Guid.NewGuid();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.DeleteAsync(id);
            var result = await userService.GetAsync(id);

            result.Should().BeNull();
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.NotFound)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Deletar usuário - Não permite apagar todos administradores")]
        public async Task DeleteUser_AdministratorLimitsAsync()
        {
            var user = _userServiceFixture.GetUserInfoAdmin();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.DeleteAsync(user.Id);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Alterar senha - Sucesso")]
        public async Task UpdatePassword_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserAdmin();
            var userPassword = new UserPassword(user.Id, user.Password, "hermes");
            var userService = _userServiceFixture.GetService(user);

            await userService.ChangePasswordAsync(userPassword);

            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Alterar senha - Erro de validação")]
        public async Task UpdatePassword_ValidationErrorAsync()
        {
            var user = _userServiceFixture.GetUserPaulo();
            var userPassword = new UserPassword(user.Id, user.Password, "123");
            var userService = _userServiceFixture.GetService(user);

            await userService.ChangePasswordAsync(userPassword);

            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Alterar senha - Não permite alterar senha de outro usuário")]
        public async Task UpdatePassword_AuthorizationErrorAsync()
        {
            var user = _userServiceFixture.GetUserPaulo();
            var userPassword = new UserPassword(user.Id, user.Password, "123456");
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.ChangePasswordAsync(userPassword);

            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Unauthorized)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Alterar senha - Senha atual inválida")]
        public async Task UpdatePassword_InvalidPasswordAsync()
        {
            var user = _userServiceFixture.GetUserAdmin();
            var userPassword = new UserPassword(user.Id, "654321", "hermes");
            var userService = _userServiceFixture.GetService(user);

            await userService.ChangePasswordAsync(userPassword);

            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Checar nome de usuário - Existe")]
        public async Task CheckUserNameExist_ExistAsync()
        {
            var user = _userServiceFixture.GetUserInfoPaulo();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var result = await userService.CheckUserNameExistAsync(user.UserName);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Checar nome de usuário - Não existe")]
        public async Task CheckUserNameExist_NotExistAsync()
        {
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var result = await userService.CheckUserNameExistAsync("invalid_user");

            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Total de usuários")]
        public async Task TotalRegistersAsync()
        {
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            var result = await userService.TotalRegistersAsync();

            result.Should().BeGreaterOrEqualTo(3);
        }
    }
}
