using Common.Helpers;
using Core.DTOs.Player;
using FluentValidation;

namespace Core.Validators
{
    public class CreatePlayerRequestValidator : AbstractValidator<CreatePlayerRequest>
    {
        public CreatePlayerRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).Must(PhoneNumberHelper.IsValidSwedishPhoneNumber);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}