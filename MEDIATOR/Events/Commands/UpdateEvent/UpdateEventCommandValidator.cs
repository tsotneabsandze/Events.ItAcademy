using System;
using FluentValidation;

namespace MEDIATOR.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500).NotEmpty();

            RuleFor(x => x.Starts > DateTime.Now).NotEmpty();
            RuleFor(x => x.Ends > DateTime.Now).NotEmpty();

            RuleFor(x => x.Starts).LessThan(x=>x.Ends);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}