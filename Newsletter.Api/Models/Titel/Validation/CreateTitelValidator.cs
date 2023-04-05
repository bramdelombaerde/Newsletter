using FluentValidation;

namespace Newsletter.Api.Models.Titel.Validation
{
    public class CreateTitelValidator : AbstractValidator<CreateTitel>
    {
        public CreateTitelValidator()
        {
            RuleFor(x => x.ShortName)
            .NotEmpty();

            RuleFor(x => x.Name)
            .NotEmpty();
        }
    }
}
