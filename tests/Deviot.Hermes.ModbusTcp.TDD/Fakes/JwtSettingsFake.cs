using Deviot.Hermes.ModbusTcp.Business.Settings;

namespace Deviot.Hermes.ModbusTcp.TDD.Fakes
{
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
