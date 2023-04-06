using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.NewsletterTemplate;
using Newsletter.Application.NewsletterTemplate;

namespace Newsletter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsletterTemplateController : NewsletterControllerBase
    {
        public NewsletterTemplateController(ISender sender) : base(sender)
        { }


        [HttpGet(Name = "GetNewsletterTemplates")]
        public async Task<IActionResult> Get()
        {
            var result = await _sender.Send(new GetAllNewsletterTemplatesQuery());

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }


        [HttpPost(Name = "CreateNewsletterTemplate")]
        public async Task<IActionResult> Post([FromBody] CreateNewsletterTemplate createNewsletterTemplate)
        {
            var result = await _sender.Send(
                new CreateNewsletterTemplateCommand(
                createNewsletterTemplate.TemplateName,
                createNewsletterTemplate.TitelId,
                createNewsletterTemplate.Html,
                createNewsletterTemplate.Tokens)
            );

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }
    }
}
