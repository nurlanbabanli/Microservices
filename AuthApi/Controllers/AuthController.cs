using AuthApi.Dtos;
using AuthApi.Entities;
using AuthApi.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper;

        public AuthController(ITokenHelper tokenHelper)
        {
            _tokenHelper=tokenHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {

            var user = new User
            {
                Id=1,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
            };

            var claims = new List<OperationClaim>
            {
                new OperationClaim{Id=1,Name="admin"}
            };

            var token = _tokenHelper.CreateToken(user, claims);

            return Ok(token);
        }


        [Authorize]
        [HttpGet("getData")]
        public async Task<IActionResult> Get()
        {
            var k = HttpContext.Request.Headers;
            string authHeader = HttpContext.Request.Headers["Authorization"];

            return Ok("This is OK");
        }
    }
}
