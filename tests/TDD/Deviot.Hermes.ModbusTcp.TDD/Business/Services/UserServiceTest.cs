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
            var user = _userServiceFixture.GetUserAdmin();
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

            resultado.Should().HaveCountGreaterOrEqualTo(2);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Inserir usuário normal - Sucesso")]
        public async Task InsertNormalUser_SuccessAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario novo", "usuario_novo", "123");
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
            var newUser = new User(Guid.NewGuid(), "Usuario administrador", "usuario_admin", "123", true, true);
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            await userService.InsertAsync(newUser);
            var result = await userService.GetAsync(newUser.Id);

            result.Should().BeEquivalentTo(newUser);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Created)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Inserir usuário - Erro de autorização")]
        public async Task InsertAdminUser_AuthorizationErrorAsync()
        {
            var newUser = new User(Guid.NewGuid(), "Usuario administrador", "usuario_admin", "123", true, true);
            var loggedUser = _userServiceFixture.GetUserNormal();
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

        [Fact(DisplayName = "Atualizar usuário - Sucesso")]
        public async Task UpdateUser_SuccessAsync()
        {
            var user = _userServiceFixture.GetUserNormal();
            var loggedUser = _userServiceFixture.GetUserNormal();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetFullName("Usuario atualizado");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.HasNotifications.Should().BeFalse();
        }

        [Fact(DisplayName = "Atualizar usuário - Erro de validação")]
        public async Task UpdateUser_ValidationErrorAsync()
        {
            var user = _userServiceFixture.GetUserNormal();
            var loggedUser = _userServiceFixture.GetUserNormal();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetUserName("usuario atualizado");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

        [Fact(DisplayName = "Atualizar usuário - Nome de usuário já existente")]
        public async Task UpdateUser_ExistingUsernameAsync()
        {
            var user = _userServiceFixture.GetUserNormal();
            var loggedUser = _userServiceFixture.GetUserNormal();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetUserName("admin");

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
            var user = _userServiceFixture.GetUserNormal();
            var userService = _userServiceFixture.GetServiceWithLoggedAdmin();

            user.SetUserName("paulo");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _userServiceFixture.HasNotifications.Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar outro usuário - Erro de autorização")]
        public async Task UpdateUserExistingUsername()
        {
            var user = _userServiceFixture.GetUserNormal();
            var loggedUser = _userServiceFixture.GetUserNormal();
            var userService = _userServiceFixture.GetService(loggedUser);

            user.SetUserName("admin");

            await userService.UpdateAsync(user);
            var result = await userService.GetAsync(user.Id);

            result.Should().NotBeEquivalentTo(user);
            _userServiceFixture.Notifications
                               .Any(x => x.Type == HttpStatusCode.Forbidden)
                               .Should()
                               .BeTrue();
        }

    }
}
