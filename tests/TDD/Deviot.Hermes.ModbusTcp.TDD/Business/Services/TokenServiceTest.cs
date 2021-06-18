using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Settings;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    public class TokenServiceTest
    {
        [Fact(DisplayName = "Gerar token")]
        public void GenerateToken()
        {
            var user = new User(Guid.NewGuid(), "Administrador", "admin", "admin", true);
            var settings = GetJwtSettings();
            var options = Options.Create(settings);
            ITokenService tokenService = new TokenService(options);

            var token = tokenService.GenerateToken(user);

            token.Id.Should().NotBeEmpty();
            token.AccessToken.Should().NotBeNullOrEmpty();
        }

        private static JwtSettings GetJwtSettings()
        {
            return new JwtSettings { Key = "SENHASUPERSECRETA", 
                                     ValidIssuer = nameof(Deviot), 
                                     ValidAudience = "https://localhost", 
                                     ExpirationTimeSeconds = 86400 
                                   };
        }

    }
}
