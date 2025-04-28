using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Internship.DAL.Repositories;
using Internship.Models;


namespace Internship.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class AuthController:ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AuthRepository _authRepo;

        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _authRepo = new AuthRepository();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if(_authRepo.ValidateUser(request.Username, request.Password))
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(token);
            }

            return Unauthorized("Invalid credentials");
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials//todo
        }

       
        
    }
}
