using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Deviot.Hermes.ModbusTcp.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        private const string CONFIG_ERROR = "As configurações do token não foram informadas";
        private const string TOKEN_ERROR = "Erro ao gerar o token.";

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            if (jwtSettings.Value is null)
                throw new Exception(CONFIG_ERROR);
            
            _jwtSettings = jwtSettings.Value;
        }

        private static ClaimsIdentity GenerateClaimsIdentity(Guid tokenId, UserInfo user)
        {
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Nbf, Utils.ToUnixEpochDate(DateTime.UtcNow).ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, Utils.ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            identityClaims.AddClaim(new Claim("token-id", tokenId.ToString()));
            identityClaims.AddClaim(new Claim("user-id", user.Id.ToString()));
            identityClaims.AddClaim(new Claim("user-fullname", user.FullName.ToString()));
            identityClaims.AddClaim(new Claim("user-username", user.UserName.ToString()));
            identityClaims.AddClaim(new Claim("user-administrator", user.Administrator.ToString()));

            if (user.Administrator)
                identityClaims.AddClaim(new Claim("role", "admin"));

            return identityClaims;
        }

        private string GenerateAccessToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.ValidIssuer,
                Audience = _jwtSettings.ValidAudience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpirationTimeSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public Token GenerateToken(UserInfo user)
        {
            try
            {
                var tokenId = Guid.NewGuid();
                var identityClaims = GenerateClaimsIdentity(tokenId, user);
                var accessToken = GenerateAccessToken(identityClaims);
                return new Token(tokenId, accessToken, user);
            }
            catch (Exception exception)
            {
                throw new Exception(TOKEN_ERROR, exception);
            }
        }

        
    }
}
