using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.Titel;
using Newsletter.Application.Titel;

namespace Newsletter.Api.Controllers
{
    [Route("titels")]
    [ApiController]
    public class TitelController : NewsletterControllerBase
    {
        public TitelController(ISender sender) : base(sender)
        { }

        [HttpGet(Name = "GetTitels")]
        public async Task<IActionResult> Get()
        {
            var result = await _sender.Send(new GetAllTitelsQuery());

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _sender.Send(new GeTitelByIdQuery(id));

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpPost(Name = "CreateTitel")]
        public async Task<IActionResult> Post([FromBody] CreateTitel createTitel)
        {
            var result = await _sender.Send(
                new CreateTitelCommand(
                createTitel.Name,
                createTitel.ShortName)
            );

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }
    }
}
