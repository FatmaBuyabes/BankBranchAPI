using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static BankBranchAPI.Controllers.LoginController;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankBranchAPI.Models
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (bool IsValid, string Token) GenerateToken(string username, string password)
        {
            if (username != "admin" || password != "admin")
            {
                return (false, "");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            { //the only  things that will change here it will save the data for the token 
        new Claim(TokenClaimsConstant.Username, username),
        new Claim(TokenClaimsConstant.UserId, "1"),
        new Claim(ClaimTypes.Role, "User")
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), //Expire 
                signingCredentials: credentials);
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, generatedToken);
        }
    }
}
