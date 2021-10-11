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

            RuleFor(x => x.Starts > DateTime.Now).NotEmpty();
            RuleFor(x => x.Ends > DateTime.Now).NotEmpty();

            RuleFor(x => x.Starts).LessThan(x=>x.Ends);
        }
    }
}