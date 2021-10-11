using FluentValidation;

namespace MEDIATOR.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryValidator : AbstractValidator<GetEventDetailQuery>
    {
        public GetEventDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}