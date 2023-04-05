using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.User;
using Newsletter.Application.User;

namespace Newsletter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : NewsletterControllerBase
    {
        public UserController(ISender sender) : base(sender)
        {
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> Post([FromBody] CreateUser createUser)
        {
            var result = await _sender.Send(
                new CreateUserCommand(createUser.Email,
                createUser.FirstName,
                createUser.LastName)
            );

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }
    }
}