using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Internship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AuthRepository _authRepo;
        private readonly PersonRepository _personRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IOptions<JwtSettings> jwtSettings, PersonRepository personRepository, ILogger<AuthController> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _authRepo = new AuthRepository();
            _personRepository = personRepository;
            _logger = logger;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //Console.WriteLine("{request.Name} {request.Password}");
            _logger.LogInformation("Login attempt for {Name} and {password}", request.Name,request.Password);
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Name and Password are required.");
            }

            if (_authRepo.ValidateUser(request.Name, request.Password))
            {
                var token = GenerateJwtToken(request.Name);
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite= SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes)
                });

                return Ok(new { token, message = "Login successful",user=request.Name });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Name and Password are required.");
            }

            // Check if LoginID already exists
            if (_personRepository.GetAllPersons().Any(p => p.LoginID == request.LoginID))
            {
                return Conflict("A user with this LoginID already exists.");
            }

            var newPerson = new Person
            {
                // Do not set PId, let the database handle this automatically
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                LoginID = request.LoginID,
                Password = request.Password,
                LoginStatus = true,  // Default to true as per your logic
                Remarks = request.Remarks
            };

            try
            {
                _personRepository.CreatePerson(newPerson);
                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // POST api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            return Ok(new { message = "Logged out successfully" });
        }

        // Generate JWT Token
        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
