using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Dtos;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApi2DataController : ControllerBase
    {
        [HttpGet("getWebApi2Data")]
        public async Task<IActionResult> GetWebApiData()
        {
            var webApiData = DataTransfer.GetData();


            return Ok(webApiData);
        }

        [HttpPost("postWebApi2Data")]
        public async Task Post([FromBody]WebApi2RequestDto webApi2RequestDto)
        {
            DataTransfer.PostData(webApi2RequestDto);
        }
    }
}
