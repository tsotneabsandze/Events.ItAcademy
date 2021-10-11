using System;
using FluentValidation;

namespace MEDIATOR.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.CanBeEditedTill)
                .Must(x => x > DateTime.Now);
        }
    }
}