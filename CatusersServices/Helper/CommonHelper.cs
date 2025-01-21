using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatUsersServices.Helper
{
    public class CommonHelper
    {
        public static string Key { 
            get {
                return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
            } 
        }
        public static string GenerateJwtToken(string user) { 
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(Key);
            var tokenDes = new SecurityTokenDescriptor() { 
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.Name, user),
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);

            return tokenHandler.WriteToken(token);
        }
    }
}
