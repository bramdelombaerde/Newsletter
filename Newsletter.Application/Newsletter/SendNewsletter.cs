using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Clients;
using Newsletter.Core.Helper;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Newsletter
{
    public record SendNewsletterCommand(Guid NewsletterId, SendVia sendVia) : IRequest<IResult<SendNewsletterResponse>>;
    public record SendNewsletterToken(string Name, string Value, string Source);
    public record SendNewsletterResponse(Guid Id);

    public class SendNewsletterHandler : IRequestHandler<SendNewsletterCommand, IResult<SendNewsletterResponse>>
    {
        private readonly Newsletters _newsletters;
        private readonly Titels _titels;
        private readonly IEmailSender _emailSender;
        private readonly IExternalClient1 _externalClient1;
        private readonly IExternalClient2 _externalClient2;

        public SendNewsletterHandler(
            Newsletters newsletters,
            Titels titels,
            IEmailSender emailSender,
            IExternalClient1 externalClient1,
            IExternalClient2 externalClient2)
        {
            _newsletters = newsletters;
            _titels = titels;
            _emailSender = emailSender;
            _externalClient1 = externalClient1;
            _externalClient2 = externalClient2;
        }
        public async Task<IResult<SendNewsletterResponse>> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
        {
            var newsletter = await _newsletters.GetById(request.NewsletterId);
            if (newsletter == null) return Result.NotFound<SendNewsletterResponse>($"NewsletterId '{request.NewsletterId}' not found");

            var titel = await _titels.GetById(newsletter.TitelId);

            if (request.sendVia == SendVia.Email)
                await SendViaEmail(newsletter, titel);

            if (request.sendVia == SendVia.ExternalService1)
                await _externalClient1.SendNewsletter(newsletter);

            if (request.sendVia == SendVia.ExternalService2)
                await _externalClient2.SendNewsletter(newsletter);

            return Result.Success(new SendNewsletterResponse(newsletter.Id));
        }

        private async Task SendViaEmail(Domain.Newsletter newsletter, Domain.Titel titel)
        {
            foreach (var subscriber in titel.Subscriptions)
            {
                await _emailSender.SendEmailAsync(
                    subscriber.User.Email,
                    $"{titel.Name} Newsletter V{newsletter.Version}",
                    newsletter.Content);
            }
        }
    }
}
