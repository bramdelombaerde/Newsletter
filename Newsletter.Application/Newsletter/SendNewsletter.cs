using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Helper;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Newsletter
{
    public record SendNewsletterCommand(Guid NewsletterId) : IRequest<IResult<SendNewsletterResponse>>;
    public record SendNewsletterToken(string Name, string Value, string Source);
    public record SendNewsletterResponse(Guid Id);

    public class SendNewsletterHandler : IRequestHandler<SendNewsletterCommand, IResult<SendNewsletterResponse>>
    {
        private readonly Newsletters _newsletters;
        private readonly Titels _titels;
        private readonly IEmailSender _emailSender;

        public SendNewsletterHandler(
            Newsletters newsletters,
            Titels titels,
            IEmailSender emailSender)
        {
            _newsletters = newsletters;
            _titels = titels;
            _emailSender = emailSender;
        }
        public async Task<IResult<SendNewsletterResponse>> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
        {
            var newsletter = await _newsletters.GetById(request.NewsletterId);
            if (newsletter == null) return Result.NotFound<SendNewsletterResponse>($"NewsletterId '{request.NewsletterId}' not found");

            var titel = await _titels.GetById(newsletter.TitelId);

            foreach (var subscriber in titel.Subscriptions)
            {
                await _emailSender.SendEmailAsync(
                    subscriber.User.Email,
                    $"{titel.Name} Newsletter V{newsletter.Version}",
                    newsletter.Content);
            }

            return Result.Success(new SendNewsletterResponse(newsletter.Id));

        }
    }
}
