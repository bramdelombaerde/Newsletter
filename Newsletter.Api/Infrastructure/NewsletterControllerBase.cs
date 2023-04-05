using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Application.Shared;
using System.Net;

namespace Newsletter.Api.Infrastructure
{
    public class NewsletterControllerBase : ControllerBase
    {
        protected readonly ISender _sender;
        public NewsletterControllerBase(ISender sender)
        {
            _sender = sender;
        }

        protected IActionResult ErrorActionResult(Application.Shared.IResult result)
        {
            switch (result.ErrorCode)
            {
                case ErrorCode.None:
                case ErrorCode.General:
                case ErrorCode.Validation:
                case ErrorCode.BusinessRule:
                    return Problem(
                        detail: result.Error,
                        statusCode: (int)HttpStatusCode.BadRequest,
                        title: result.ErrorCode.ToString());
                case ErrorCode.NotFound:
                    return Problem(
                        detail: result.Error,
                        statusCode: (int)HttpStatusCode.NotFound,
                        title: result.ErrorCode.ToString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
