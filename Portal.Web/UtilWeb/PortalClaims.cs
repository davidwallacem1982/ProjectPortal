using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portal.Core.Identity;
using Portal.Web.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portal.Web.UtilWeb
{
    public class PortalClaims
    {
        private readonly IConfiguration _configuration;
        public PortalClaims(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public UserToken BuildToken(ApplicationUser userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName),
                new Claim("UsuarioId", userInfo.Id.ToString()),
                new Claim("ClienteId", userInfo.ClientesID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 30 minutos
            //var expiration = DateTime.UtcNow.AddHours(1);
            var expiration = DateTime.UtcNow.AddMinutes(30);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
