using FluentValidation;

namespace Newsletter.Api.Models.Newsletter.Validation
{
    public class CreateNewsletterValidator : AbstractValidator<CreateNewsletter>
    {
        public CreateNewsletterValidator()
        {
            RuleFor(x => x.TemplateId)
            .NotEmpty();

            RuleFor(x => x.TitelId)
            .NotEmpty();

            RuleForEach(x => x.Tokens)
                .SetValidator(new NewsletterTokenValidator());
        }
    }

    public class NewsletterTokenValidator : AbstractValidator<NewsletterToken>
    {
        public NewsletterTokenValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Source)
                .NotNull();
        }
    }
}
