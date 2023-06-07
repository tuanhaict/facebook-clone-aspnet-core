using Fadebook.Application.Models.TokenModel;
using Fadebook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Extensions
{
    public static class AuthExtension
    {
       public static Token GenerateToken(Guid userId, IConfiguration configuration)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };
            var jwtSettings = configuration.GetSection("JwtSettings");
            var issuer = jwtSettings.GetValue<string>("ValidIssuer");
            var audience = jwtSettings.GetValue<string>("ValidAudience");
            var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("SecretKey"));
            var accessToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );
            var refreshToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );
            var encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var encodedRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return new Token { AccessToken = encodedAccessToken, RefreshToken = encodedRefreshToken };

        }
        public static ClaimsPrincipal GetPrincipalFromToken(string token, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetValue<string>("ValidIssuer"),
                ValidAudience = jwtSettings.GetValue<string>("ValidAudience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("SecretKey"))),
            };
            var tokenHanlder = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHanlder.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;

        }
        public static Guid GetUserIdFromTokenFromContext(HttpContext httpContext, IConfiguration configuration)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            return GetUserIdFromToken(token, configuration);

        }
        public static Guid GetUserIdFromToken(string token, IConfiguration configuration)
        {
            var principal = GetPrincipalFromToken(token, configuration);
            var userId = principal.Claims.ToList().FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(userId);
        }
        
    }
}
