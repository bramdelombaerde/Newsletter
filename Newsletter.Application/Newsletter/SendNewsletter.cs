using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Newsletter
{
    public record SendNewsletterCommand(Guid NewsletterId) : IRequest<IResult<SendNewsletterResponse>>;
    public record SendNewsletterToken(string Name, string Value, string Source);
    public record SendNewsletterResponse(Guid Id);

    public class SendNewsletterHandler : IRequestHandler<SendNewsletterCommand, IResult<SendNewsletterResponse>>
    {
        private readonly Newsletters _newsletters;

        public SendNewsletterHandler(
            Newsletters newsletters)
        {
            _newsletters = newsletters;
        }
        public async Task<IResult<SendNewsletterResponse>> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
        {
            var newsletter = await _newsletters.GetById(request.NewsletterId);
            if (newsletter == null) return Result.NotFound<SendNewsletterResponse>($"NewsletterId '{request.NewsletterId}' not found");





            return Result.Success(new SendNewsletterResponse(newsletter.Id));

        }
    }
}
