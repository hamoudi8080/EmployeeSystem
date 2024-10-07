using EmployeeManagmentApi.Auth;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagmentApi.Controllers
{



    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IAuthService authService;

        public AuthController(IConfiguration config, IAuthService authService)
        {
            this.config = config;
            this.authService = authService;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
             //   Admin user = await authService.ValidateUser(userLoginDto.Username, userLoginDto.Password);
                Admin user = await authService.GetUser(userLoginDto.Username, userLoginDto.Password);

                if (user == null)
                {
                    return Unauthorized();
                }


                string token = GenerateJwt(user); // <-- Add this line

               

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string GenerateJwt(Admin user)
        {
            List<Claim> claims = GenerateClaims(user);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtHeader header = new JwtHeader(signIn);

            JwtPayload payload = new JwtPayload(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                null,
                DateTime.UtcNow.AddMinutes(60));

            JwtSecurityToken token = new JwtSecurityToken(header, payload);

            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return serializedToken;
        }

        private List<Claim> GenerateClaims(Admin user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("DisplayName", user.Name),
            new Claim("Email", user.Email),
            new Claim("Age", user.Age.ToString()),
            new Claim("Domain", user.Domain),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
            return claims.ToList();
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> RegisterAdmin(  Admin admin)
        {
            try
            {
                Admin user = await authService.RegisterAdmin(admin);
               
                return Ok(user);

 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
