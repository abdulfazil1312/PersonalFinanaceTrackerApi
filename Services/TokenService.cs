using ExpenseTrackerAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        // The method that does all the work. It takes a User object...
        public string CreateToken(User user)
        {

            // A "claim" is a statement about the user (e.g., "their email is...").
            // These get encoded into the token.
            var claims = new List<Claim>
            {
                // This is the user's unique ID. It's the primary way we'll identify them.
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // The key is the secret used to sign the token.
            // We pull our secret key from appsettings.json and create a security key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // We use the key to create signing credentials with a strong hashing algorithm.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // This object describes the token we want to create.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // Token is valid for 7 days
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            // This creates the token based on our descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // This writes the token object into the final string format (e.g., "eyJ...").
            return tokenHandler.WriteToken(token);
        }
    }
}
