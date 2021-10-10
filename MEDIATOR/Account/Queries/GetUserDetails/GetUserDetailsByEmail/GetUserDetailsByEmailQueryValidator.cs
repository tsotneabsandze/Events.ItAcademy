using FluentValidation;

namespace MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsByEmail
{
    public class GetUserDetailsByEmailQueryQueryValidator : AbstractValidator<GetUserDetailsByEmailQuery>
    {
        public GetUserDetailsByEmailQueryQueryValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}