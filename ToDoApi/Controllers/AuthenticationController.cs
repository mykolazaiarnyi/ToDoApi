using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApi.Infrastructure.Authentication;

namespace ToDoApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> CreateUserAsync(CreateUserDto userDto)
        {
            var result = await _authenticationService.CreateUserAsync(userDto);
            return Ok(result);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUserAsync(AuthenticateUserDto userDto)
        {
            var user = await _authenticationService.AuthenticateUserAsync(userDto);
            if (user is null)
            {
                return BadRequest("Login or password is invalid");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JwtToken")["Key"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            return Ok(tokenHandler.CreateEncodedJwt(tokenDescriptor));
        }
    }
}
