using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;
using Core.Extensions;
using Core.Utilities.Helper;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private TokenOptions tokenOptions;
        private DateTime accessTokenExpiration;

        public JwtHelper()
        {
            tokenOptions = ConfigHelper.GetConfigSection<TokenOptions>("TokenOptions");
        }

        public AccessToken CreateToken(long UserId, string Email, string FirstName, string LastName, List<ClaimModel> operationClaims)
        {
            accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(tokenOptions, UserId, Email, FirstName, LastName, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out _);

            return true;
        }

        public RefreshToken CreateRefreshToken(long UserId, string ipAddress)
        {
            RefreshToken refreshToken = new()
            {
                UserId = UserId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, long UserId, string Email, string FirstName, string LastName, 
            SigningCredentials signingCredentials, List<ClaimModel> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: SetClaims(UserId, Email, FirstName, LastName, operationClaims),
                signingCredentials:signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(long UserId, string Email, string FirstName, string LastName, List<ClaimModel> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(UserId.ToString());
            claims.AddEmail(Email);
            claims.AddName($"{FirstName} {LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());
            
            return claims;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false, // Token'ın yayıncısını doğrula
                ValidateAudience = false, // Token'ın hedef kitlesini doğrula
                ValidateLifetime = true, // Token'ın süresini doğrula
                ValidateIssuerSigningKey = true, // İmza anahtarını doğrula

                ValidIssuer = tokenOptions.Issuer, // Doğrulanacak yayıncıyı belirtin (opsiyonel)
                ValidAudience = tokenOptions.Audience, // Doğrulanacak hedef kitleyi belirtin (opsiyonel)
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey) // İmza anahtarını belirtin
            };
        }
    }
}
