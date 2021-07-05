using Deviot.Hermes.ModbusTcp.Business.Settings;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
    [ExcludeFromCodeCoverage]
    public static class JwtSettingsFake
    {
        private const string SECRET = "SENHASUPERSECRETA";
        private const string URL = "https://localhost";

        public static JwtSettings GetJwtSettings(int expirationTimeSeconds = 86400)
        {
            return new JwtSettings
            {
                Key = SECRET,
                ValidIssuer = nameof(Deviot),
                ValidAudience = URL,
                ExpirationTimeSeconds = expirationTimeSeconds
            };
        }
    }
}
