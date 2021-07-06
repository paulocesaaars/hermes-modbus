using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Services;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(AuthServiceCollection))]
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
            var login = _authServiceFixture.GetLoginAdmin();

            var token = await _authService.LoginAsync(login);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeEmpty();
            token.User.Should().NotBeNull();
        }

        [Fact(DisplayName = "Login inválido")]
        public async Task InvalidLogin()
        {
            var login = _authServiceFixture.GetInvalidLogin();
            var token = await _authService.LoginAsync(login);

            token.Should().BeNull();
        }

        [Fact(DisplayName = "Parametrizando usuário logado")]
        public void GetSetLoggedUser()
        {
            var user = _authServiceFixture.GetUserInfoAdmin();

            _authService.SetLoggedUser(user);
            var result = _authService.GetLoggedUser();

            _authService.IsAuthenticated.Should().BeTrue();
            result.Should().BeEquivalentTo(user);
        }
    }
}
