using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Newsletter
{
    public record CreateNewsletterCommand(Guid TitelId, Guid TemplateId, List<CreateNewsletterToken> Tokens) : IRequest<IResult<CreateNewsletterResponse>>;
    public record CreateNewsletterToken(string Name, string Value, Source Source);
    public record CreateNewsletterResponse(Guid Id);

    public class CreateNewsletterHandler : IRequestHandler<CreateNewsletterCommand, IResult<CreateNewsletterResponse>>
    {
        private readonly Newsletters _newsletters;
        private readonly NewsletterTemplates _newsletterTemplates;
        private readonly Titels _titels;

        public CreateNewsletterHandler(
            Newsletters newsletters,
            NewsletterTemplates newsletterTemplates,
            Titels titels)
        {
            _newsletters = newsletters;
            _newsletterTemplates = newsletterTemplates;
            _titels = titels;
        }
        public async Task<IResult<CreateNewsletterResponse>> Handle(CreateNewsletterCommand request, CancellationToken cancellationToken)
        {
            var titel = await _titels.GetById(request.TitelId);
            if (titel == null) return Result.NotFound<CreateNewsletterResponse>($"TitelId '{request.TitelId}' not found");

            var newsletterTemplate = await _newsletterTemplates.GetById(request.TemplateId);
            if (newsletterTemplate == null) return Result.NotFound<CreateNewsletterResponse>($"TemplateId '{request.TemplateId}' not found");

            var nextVersionNumber = await _newsletters.GetNextVersionNumberForTitel(request.TitelId);
            var content = newsletterTemplate.Html;

            foreach (var token in request.Tokens)
            {
                content = content.Replace("{{" + token.Name + "}}", token.Value);
            }


            var newsletter = new Domain.Newsletter(titel, nextVersionNumber, content);
            await _newsletters.Create(newsletter);
            await _newsletters.SaveChangesAsync(cancellationToken);

            return Result.Success(new CreateNewsletterResponse(newsletter.Id));

        }
    }
}
