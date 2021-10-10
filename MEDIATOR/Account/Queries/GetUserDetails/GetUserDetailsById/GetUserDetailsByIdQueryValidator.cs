using FluentValidation;

namespace MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsById
{
    public class GetUserDetailsByIdQueryValidator : AbstractValidator<GetUserDetailsByIdQuery>
    {
        public GetUserDetailsByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}