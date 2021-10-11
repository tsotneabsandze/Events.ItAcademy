using System;
using FluentValidation;

namespace MEDIATOR.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Description).MaximumLength(500).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(30).NotEmpty();
            RuleFor(x => x.CanBeEditedTill)
                .Must(x => x > DateTime.Now);
        }
    }
}