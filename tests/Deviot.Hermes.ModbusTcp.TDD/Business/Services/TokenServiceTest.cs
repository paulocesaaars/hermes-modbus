using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    [ExcludeFromCodeCoverage]
    public class TokenServiceTest
    {
        private readonly ITokenService _tokenService;

        public TokenServiceTest()
        {
            var jwtSettings = JwtSettingsHelper.GetJwtSettings();
            var options = Options.Create(jwtSettings);
            _tokenService = new TokenService(options);
        }

        [Fact(DisplayName = "Gerar token válido")]
        public void GenerateToken()
        {
            var user = UserFake.GetUserAdmin();
            var token = _tokenService.GenerateToken(user);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeNullOrEmpty();
            token.User.Should().BeEquivalentTo(user);
        }
    }
}
