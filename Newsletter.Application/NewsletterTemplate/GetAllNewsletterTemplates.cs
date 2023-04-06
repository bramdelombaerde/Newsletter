using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.NewsletterTemplate;

public record GetAllNewsletterTemplatesQuery() : IRequest<IResult<List<NewsletterTemplateListModel>>>;
public record NewsletterTemplateListModel(Guid Id, string TemplateName, string Html, List<string> Tokens, Guid TitelId);

public class GetAllNewsletterTemplates : IRequestHandler<GetAllNewsletterTemplatesQuery, IResult<List<NewsletterTemplateListModel>>>
{
    private readonly NewsletterTemplates _NewsletterTemplates;

    public GetAllNewsletterTemplates(NewsletterTemplates NewsletterTemplates)
    {
        _NewsletterTemplates = NewsletterTemplates;
    }
    public async Task<IResult<List<NewsletterTemplateListModel>>> Handle(GetAllNewsletterTemplatesQuery request, CancellationToken cancellationToken)
    {
        var NewsletterTemplates = await _NewsletterTemplates.GetAll();

        var mappedNewsletterTemplates = NewsletterTemplates
            .Select(x => new NewsletterTemplateListModel(x.Id, x.TemplateName, x.Html, x.Tokens, x.TitelId))
            .ToList();

        return Result.Success(mappedNewsletterTemplates);
    }
}

