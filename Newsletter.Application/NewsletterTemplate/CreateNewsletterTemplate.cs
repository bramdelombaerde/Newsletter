using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.NewsletterTemplate
{
    public record CreateNewsletterTemplateCommand(string TemplateName, Guid TitelId, string Html, List<string> Tokens) : IRequest<IResult<CreateNewsletterTemplateResponse>>;
    public record CreateNewsletterTemplateResponse(Guid Id, string TemplateName);

    public class CreateNewsletterTemplateHandler : IRequestHandler<CreateNewsletterTemplateCommand, IResult<CreateNewsletterTemplateResponse>>
    {
        private readonly NewsletterTemplates _newsletterTemplates;
        private readonly Titels _titels;

        public CreateNewsletterTemplateHandler(
            NewsletterTemplates NewsletterTemplates,
            Titels titels)
        {
            _newsletterTemplates = NewsletterTemplates;
            _titels = titels;
        }
        public async Task<IResult<CreateNewsletterTemplateResponse>> Handle(CreateNewsletterTemplateCommand request, CancellationToken cancellationToken)
        {
            var titel = await _titels.GetById(request.TitelId);
            if (titel == null) return Result.NotFound<CreateNewsletterTemplateResponse>($"TitelId '{request.TitelId}' not found");

            if (await _newsletterTemplates.DoesNewsletterTemplateAlreadyExist(request.TemplateName, request.TitelId))
                return Result.BusinessRuleError<CreateNewsletterTemplateResponse>("This NewsletterTemplate already exists");

            var NewsletterTemplate = new Domain.NewsletterTemplate(request.TemplateName, titel, request.Html, request.Tokens);
            await _newsletterTemplates.Create(NewsletterTemplate);
            await _newsletterTemplates.SaveChangesAsync(cancellationToken);

            return Result.Success(new CreateNewsletterTemplateResponse(NewsletterTemplate.Id, NewsletterTemplate.TemplateName));

        }
    }
}
