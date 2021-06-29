using Deviot.Hermes.ModbusTcp.Business.Settings;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class JwtSettingsHelper
    {
        public static JwtSettings GetJwtSettings(int expirationTimeSeconds = 86400)
        {
            return new JwtSettings
            {
                Key = "SENHASUPERSECRETA",
                ValidIssuer = nameof(Deviot),
                ValidAudience = "https://localhost",
                ExpirationTimeSeconds = expirationTimeSeconds
            };
        }
    }
}
