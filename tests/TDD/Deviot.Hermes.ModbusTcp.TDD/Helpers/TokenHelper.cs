using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class TokenHelper
    {
        public static Token GetToken(UserInfo user)
        {
            var tokenService = GetTokenService();
            return tokenService.GenerateToken(user);
        }

        public static TokenService GetTokenService()
        {
            var jwtSettings = JwtSettingsHelper.GetJwtSettings();
            var options = Options.Create(jwtSettings);
            return new TokenService(options);
        }
    }
}
