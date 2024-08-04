using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OtaghakChallenge.Domain.Idp;

namespace OtaghakChallenge.Application.Idp
{
    public class TokenService : ITokenService
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
    {
        { "TestCustomer","TestCustomer"},
        { "TestAdmin","TestAdmin"},
        { "string","string"},
    };

        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public Tokens Authenticate(User user)
        {
           
            // Check if the user exists and retrieve their role
            if (!UsersRecords.TryGetValue(user.Name, out var storedPassword) || storedPassword != user.Password)
            {
                return null; // Invalid credentials
            }

            // Determine the user's role (this is a simple example; adapt as necessary)
            string userRole = GetUserRole(user.Name); // Implement this method to get the user's role

            // Generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, userRole) ,
            // Add the role claim
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }

        private string GetUserRole(string username)
        {
            if (username == "TestAdmin")
            {
                return "Admin";
            }

            else if (username == "TestCustomer")
            {
                return "Customer";
            }
            else
            {
                return "Customer"; // Default role
            }

        }
    }
}
