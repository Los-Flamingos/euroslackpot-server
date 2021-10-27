using Core.DTOs.Number;
using FluentValidation;

namespace Core.Validators
{
    public class CreateNumberRequestValidator : AbstractValidator<CreateNumberRequest>
    {
        public CreateNumberRequestValidator()
        {
            RuleFor(x => x.RowId).NotEmpty().WithMessage("Row id cannot be default value (0)");
            RuleFor(x => x.NumberRequest.Week).NotEmpty().WithMessage("Week cannot be default value (0)");
            RuleFor(x => x.NumberRequest.Value).NotEmpty().WithMessage("Value cannot be default value (0)");
            RuleFor(x => x.NumberRequest.PlayerId).NotEmpty().WithMessage("Player Id cannot be default value (0)");
        }
    }
}
