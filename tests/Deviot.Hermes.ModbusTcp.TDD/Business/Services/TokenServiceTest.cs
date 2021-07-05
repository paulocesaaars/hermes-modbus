using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
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
            var jwtSettings = JwtSettingsFake.GetJwtSettings();
            var options = Options.Create(jwtSettings);
            _tokenService = new TokenService(options);
        }

        private static UserInfo GetUserInfoAdmin()
        {
            return new UserInfo(new Guid("7011423f65144a2fb1d798dec19cf466"),
                                     "Administrador",
                                     "admin",
                                     true,
                                     true);
        }

        [Fact(DisplayName = "Gerar token válido")]
        public void GenerateToken()
        {
            var user = GetUserInfoAdmin();
            var token = _tokenService.GenerateToken(user);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeNullOrEmpty();
            token.User.Should().BeEquivalentTo(user);
        }
    }
}
