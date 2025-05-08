using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;


namespace Internship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController:ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AuthRepository _authRepo;
        private readonly PersonRepository _personRepository;

        public AuthController(IOptions<JwtSettings> jwtSettings, PersonRepository personRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _authRepo = new AuthRepository();
            _personRepository = personRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if(_authRepo.ValidateUser(request.Name, request.Password))
            {
                var token = GenerateJwtToken(request.Name);
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes)
                });


                // Return success message instead of the token
                return Ok(new { message = "Login successful" });
            }

            return Unauthorized("Invalid credentials");
        }

        

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);  // Return the token
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Basic validation
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Name and Password are required.");

            // Check if LoginID already exists
            if (_personRepository.GetAllPersons().Any(p => p.LoginID == request.LoginID))//line 85
                return Conflict("A user with this LoginID already exists.");

            // Map RegisterRequest to Person entity
            var newPerson = new Person
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                LoginID = request.LoginID,
                Password = request.Password,
                LoginStatus = true,
                Remarks = request.Remarks
            };

            try
            {
                _personRepository.CreatePerson(newPerson);//line 102
                return Ok(new { message = "Registration successful" });
            }
            catch
            {
                return StatusCode(500, "Registration failed due to a server error.");
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            return Ok(new { message = "Logged out successfully" });
        }


    }
}
