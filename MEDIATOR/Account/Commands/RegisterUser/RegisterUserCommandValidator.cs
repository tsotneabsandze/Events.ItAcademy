using FluentValidation;

namespace MEDIATOR.Account.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}