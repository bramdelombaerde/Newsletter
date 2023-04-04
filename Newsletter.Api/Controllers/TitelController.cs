using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Newsletter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TitelController : ControllerBase
    {
        [HttpGet(Name = "GetTitels")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
