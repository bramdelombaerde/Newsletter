using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Models.Titel;
using Newsletter.Application.Titel;

namespace Newsletter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TitelController : ControllerBase
    {
        private readonly ISender _sender;

        public TitelController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet(Name = "GetTitels")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost(Name = "CreateTitel")]
        public async Task<IActionResult> Post([FromBody] CreateTitel createTitel)
        {
            var result = await _sender.Send(
                new CreateTitelCommand(
                createTitel.Name, 
                createTitel.ShortName)
            );

            return Ok(result);
        }
    }
}
