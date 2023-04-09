using FluentValidation;

namespace Newsletter.Api.Models.NewsletterTemplate.Validation
{
    public class CreateNewsletterTemplateValidator : AbstractValidator<CreateNewsletterTemplate>
    {
        public CreateNewsletterTemplateValidator()
        {
            RuleFor(x => x.Html)
            .NotEmpty();

            RuleFor(x => x.TemplateName)
            .NotEmpty();

            RuleFor(x => x.TitelId)
            .NotEmpty();
        }
    }
}
