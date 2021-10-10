using System.Data;
using FluentValidation;

namespace MEDIATOR.Account.Commands.SignInUser
{
    public class SignInCommandValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInCommandValidator()
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