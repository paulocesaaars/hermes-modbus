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

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public Token GenerateToken(User user)
        {
            try
            {
                var id = Guid.NewGuid();
                var identityClaims = GenerateClaimsIdentity(id, user);
                var accessToken = GenerateAccessToken(identityClaims);
                return new Token(id, accessToken);
            }
            catch (Exception exception)
            {
                throw new Exception("Erro ao gerar o token.", exception);
            }
        }

        private static ClaimsIdentity GenerateClaimsIdentity(Guid id, User user)
        {
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Nbf, Utils.ToUnixEpochDate(DateTime.UtcNow).ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, Utils.ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            identityClaims.AddClaim(new Claim("token-id", id.ToString()));
            identityClaims.AddClaim(new Claim("user-id", user.Id.ToString()));

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
    }
}
