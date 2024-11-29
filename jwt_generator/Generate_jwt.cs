using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_app.jwt_generator
{
    public interface IGenerate_jwt
    {
      string Generate_jwt_method(User data);
    }
    public class Generate_jwt:IGenerate_jwt
    {
        private readonly IConfiguration conf;  
        public Generate_jwt(IConfiguration conf) {
            this.conf = conf;
        }

        public string Generate_jwt_method(User data)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                new Claim(ClaimTypes.Name, data.Name),
                new Claim(ClaimTypes.Role, data.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Signkey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               signingCredentials: cred,
               claims: claims,
               expires: DateTime.Now.AddDays(1)

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
