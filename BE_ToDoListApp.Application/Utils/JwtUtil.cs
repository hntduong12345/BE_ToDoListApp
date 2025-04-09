using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.Domain.Enums.UserEnums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Utils
{
    public static class JwtUtil
    {
        public static string GenerateJwtToken(User user)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            var key = config["Authentication:Key"];
            var issuer = config["Authentication:Issuer"];
            var audience = config["Authentication:Audience"];

            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Role, EnumUtil.GetEnumName<UserRoleEnum>(user.Role))
            };

            //Add expiredTime of token
            var expires = DateTime.Now.AddDays(1);

            //Create token
            var token = new JwtSecurityToken(issuer, audience, claims, notBefore: DateTime.Now, expires, credentials);
            return jwtHandler.WriteToken(token);
        }
    }
}
