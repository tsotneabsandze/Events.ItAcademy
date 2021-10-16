using System;
using FluentValidation;

namespace MEDIATOR.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            
            RuleFor(x => x.Description).MaximumLength(500)
                .WithMessage("description can be maximum of 500 characters long")
                .NotEmpty();

            RuleFor(x => x.Starts)
                .GreaterThan(DateTime.Now)
                .WithMessage("starting time should not be less than current time")
                .NotEmpty();
            
            RuleFor(x => x.Ends).GreaterThan(DateTime.Now)
                .WithMessage("invalid date")
                .NotEmpty();

            RuleFor(x => x.Starts)
                .LessThan(x=>x.Ends)
                .WithMessage("invalid date");
            
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id must be provided");
        }
    }
}