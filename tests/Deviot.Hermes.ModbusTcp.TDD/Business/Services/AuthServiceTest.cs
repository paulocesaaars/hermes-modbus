using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Collections;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Services;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    [Collection(nameof(ServicesCollection))]
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        private readonly AuthServiceFixture _authServiceFixture;

        public AuthServiceTest(AuthServiceFixture authServiceFixture)
        {
            _authService = authServiceFixture.GetService();
            _authServiceFixture = authServiceFixture;
        }

        [Fact(DisplayName = "Login válido")]
        public async Task ValidLogin()
        {
            var login = LoginFake.GetLoginAdmin();

            var token = await _authService.LoginAsync(login);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeEmpty();
            token.User.Should().NotBeNull();
        }

        [Fact(DisplayName = "Login inválido")]
        public async Task InvalidLogin()
        {
            var login = new Login("login invalido", "invalido");
            var token = await _authService.LoginAsync(login);

            token.Should().BeNull();
        }

        [Fact(DisplayName = "Parametrizando usuário logado")]
        public void GetSetLoggedUser()
        {
            var user = UserInfoFake.GetUserAdmin();

            _authService.SetLoggedUser(user);
            var result = _authService.GetLoggedUser();

            _authService.IsAuthenticated.Should().BeTrue();
            result.Should().BeEquivalentTo(user);
        }
    }
}
