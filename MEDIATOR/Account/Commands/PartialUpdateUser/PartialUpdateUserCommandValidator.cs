using FluentValidation;

namespace MEDIATOR.Account.Commands.PartialUpdateUser
{
    public class PartialUpdateUserCommandValidator : AbstractValidator<PartialUpdateUserCommand>
    {
        public PartialUpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();
            
        }
    }
}