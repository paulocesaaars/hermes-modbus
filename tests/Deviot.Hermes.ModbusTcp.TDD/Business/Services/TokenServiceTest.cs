using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    public class TokenServiceTest
    {
        private readonly ITokenService _tokenService;

        public TokenServiceTest()
        {
            var jwtSettings = JwtSettingsFake.GetJwtSettings();
            var options = Options.Create(jwtSettings);
            _tokenService = new TokenService(options);
        }

        [Fact(DisplayName = "Gerar token válido")]
        public void GenerateToken()
        {
            var user = UserInfoFake.GetUserAdmin();
            var token = _tokenService.GenerateToken(user);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeNullOrEmpty();
            token.User.Should().BeEquivalentTo(user);
        }
    }
}
