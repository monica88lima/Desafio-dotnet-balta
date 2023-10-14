using DesafioDotnet_balta.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioDotnet_balta.Service
{
    public static class TokenService
    {
        public static string Generate(UserModel user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Configuration.PrivateKey);

           var credentials= new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),

            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(UserModel user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(
                new Claim(type:ClaimTypes.Name, value:user.Email));
            
                      
            
            return ci;
        }
    }
}
