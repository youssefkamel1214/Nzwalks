using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Nzwalks_api.Repostories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nzwalks_api.Repostories
{
    public class TokenRepository : ITokenRepository
    {
        public TokenRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
    

    private readonly IConfiguration _configuration;

    string ITokenRepository.CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["jwt:Key"])); // Replace with your actual secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            Console.WriteLine(_configuration["jwt:audience"]);
            Console.WriteLine(_configuration["jwt:audience"]);
            Console.WriteLine("\n\n\n\n\n\n\n");
            var token = new JwtSecurityToken(
                _configuration["jwt:Issuer"],
                _configuration["jwt:audience"],
               claims,
                expires: DateTime.Now.AddMinutes(15), // Set the token expiration time
                signingCredentials: creds
            );
            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
