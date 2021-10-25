using Core.DTOs.Row;
using FluentValidation;

namespace Core.Validators
{
    public class CreateRowRequestValidator : AbstractValidator<CreateRowRequest>
    {
        public CreateRowRequestValidator()
        {
            RuleFor(x => x.Week).NotEmpty().WithMessage("Week cannot be default value (0)");
        }
    }
}