using System.Data;
using FluentValidation;

namespace MEDIATOR.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}