using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestLib;
using WebApi1.Dtos;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApi1DataController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHubContext<MySignalRHub> _hubContext;

        public WebApi1DataController(IConfiguration config, IHttpContextAccessor contextAccessor, IHubContext<MySignalRHub> hubContext)
        {
            _config=config;
            _contextAccessor=contextAccessor;
            _hubContext=hubContext;
        }

        [Authorize()]
        [HttpGet("getWebApi1Data")]
        public async Task<IActionResult> GetWebApi1Data()
        {
            var webApi1Data = DataTransfer.GetData();

           var k= HttpContext.Request.Headers;

            string authHeader = HttpContext.Request.Headers["Authorization"];

            var roleClaims = _contextAccessor.HttpContext.User.FindAll("roles").Select(x=>x.Value).ToList();

            return Ok(webApi1Data);
        }

        [HttpGet("getSignalRMessage")]
        public async Task<IActionResult> GetSignalRMessage()
        {

            Task.Run( async() =>
            {
                int count = 0;

                while (true)
                {

                    await _hubContext.Clients.All.SendAsync("GetMessageFromFub", " This is SignalR message=>"+count.ToString());

                    await Task.Delay(1000);
                    count++;
                }

                
            });

            

            return Ok();
        }

        [HttpPost("postWebApi1Data")]
        public async Task PostWebApi1Data([FromBody]WebApi1RequestDto webApi1RequestDto)
        {
            DataTransfer.PostData(webApi1RequestDto);
        }


        [HttpPost("validateToken")]
        public async Task<IActionResult> ValidateToken([FromBody] TokenToValidate tokenToValidate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal;

            var tokenOptions = _config.GetSection("TokenOptions").Get<TokenOptions>();

            if (!tokenHandler.CanReadToken(tokenToValidate.Token))
            {
                return null;
            }

            try
            {
                principal = tokenHandler.ValidateToken(tokenToValidate.Token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                }, out securityToken);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return Ok(tokenToValidate);
        }

    }

    
}
