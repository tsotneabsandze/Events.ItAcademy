using FluentValidation;

namespace MEDIATOR.Account.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUser.DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}