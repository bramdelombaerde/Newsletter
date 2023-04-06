using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.NewsletterTemplate
{
    public record CreateNewsletterTemplateCommand(string Email, string FirstName, string LastName) : IRequest<IResult<CreateNewsletterTemplateResponse>>;
    public record CreateNewsletterTemplateResponse(Guid Id, string Email);

    public class CreateNewsletterTemplateHandler : IRequestHandler<CreateNewsletterTemplateCommand, IResult<CreateNewsletterTemplateResponse>>
    {
        private readonly NewsletterTemplates _NewsletterTemplates;

        public CreateNewsletterTemplateHandler(NewsletterTemplates NewsletterTemplates)
        {
            _NewsletterTemplates = NewsletterTemplates;
        }
        public async Task<IResult<CreateNewsletterTemplateResponse>> Handle(CreateNewsletterTemplateCommand request, CancellationToken cancellationToken)
        {
            if (await _NewsletterTemplates.DoesNewsletterTemplateAlreadyExist(request.Email))
                return Result.BusinessRuleError<CreateNewsletterTemplateResponse>("This NewsletterTemplate already exists");

            var NewsletterTemplate = new Domain.NewsletterTemplate(request.Email, request.FirstName, request.LastName);
            await _NewsletterTemplates.Create(NewsletterTemplate);
            await _NewsletterTemplates.SaveChangesAsync(cancellationToken);

            return Result.Success(new CreateNewsletterTemplateResponse(NewsletterTemplate.Id, NewsletterTemplate.Email));

        }
    }
}
