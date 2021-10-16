using System;
using FluentValidation;

namespace MEDIATOR.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("description can be maximum of 500 characters long")
                .NotEmpty();
            
            RuleFor(x => x.Title)
                .MaximumLength(30)
                .WithMessage("Title should contain less than 30 characters")
                .NotEmpty();

            RuleFor(x => x.Starts).GreaterThan(DateTime.Now)
                .WithMessage("starting time should not be less than current time")
                .NotEmpty();
            
            RuleFor(x => x.Ends).GreaterThan(DateTime.Now)
                .WithMessage("invalid date")
                .NotEmpty();

            RuleFor(x => x.Starts)
                .LessThan(x=>x.Ends)
                .WithMessage("invalid date");
        }
    }
}