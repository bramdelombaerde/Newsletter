using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.User;
using Newsletter.Application.User;

namespace Newsletter.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : NewsletterControllerBase
    {
        public UserController(ISender sender) : base(sender)
        {
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var result = await _sender.Send(new GetAllUsersQuery());

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpPost(Name = "AddUser")]
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

        [HttpPost("{userId:guid}/subscribe/{titelId:guid}", Name = "CreateSubscription")]
        public async Task<IActionResult> Subscribe(Guid userId, Guid titelId)
        {
            var result = await _sender.Send(
                new CreateSubscriptionCommand(userId,
                titelId)
            );

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok(result.Value);
        }

        [HttpDelete("{userId:guid}/unsubscribe/{titelId:guid}", Name = "RemoveSubscription")]
        public async Task<IActionResult> Unsubscribe(Guid userId, Guid titelId)
        {
            var result = await _sender.Send(
                new RemoveSubscriptionCommand(userId,
                titelId)
            );

            return result.IsFailure
            ? ErrorActionResult(result)
            : Ok();
        }


    }
}