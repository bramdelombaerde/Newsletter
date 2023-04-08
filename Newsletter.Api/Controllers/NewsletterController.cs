using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.Newsletter;
using Newsletter.Application.Newsletter;

namespace Newsletter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsletterController : NewsletterControllerBase
    {
        public NewsletterController(ISender sender) : base(sender)
        {
        }

        [HttpPost(Name = "CreateNewsletter")]
        public async Task<IActionResult> Post([FromBody] CreateNewsletter createNewsletter)
        {
            var result = await _sender.Send(
                new CreateNewsletterCommand(
                    createNewsletter.TitelId,
                    createNewsletter.TemplateId,
                    createNewsletter.Tokens.Select(x => new CreateNewsletterToken(
                        x.Name,
                        x.Value,
                        x.Source)).ToList()
            ));

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpPost("{newsletterId:guid}/archive", Name = "ArchiveNewsletter")]
        public async Task<IActionResult> ArchiveNewsletter(Guid newsletterId)
        {
            var result = await _sender.Send(
                new ArchiveNewsletterCommand(newsletterId));

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpPost("{newsletterId:guid}/Send", Name = "SendNewsletter")]
        public async Task<IActionResult> SendNewsletter(Guid newsletterId)
        {
            var result = await _sender.Send(
                new SendNewsletterCommand(newsletterId));

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }
    }
}
