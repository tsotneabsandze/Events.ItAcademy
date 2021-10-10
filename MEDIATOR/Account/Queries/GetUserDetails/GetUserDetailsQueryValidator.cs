using FluentValidation;

namespace MEDIATOR.Account.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}