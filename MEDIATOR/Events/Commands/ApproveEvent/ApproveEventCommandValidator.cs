using System;
using System.Data;
using FluentValidation;

namespace MEDIATOR.Events.Commands.ApproveEvent
{
    public class ApproveEventCommandValidator : AbstractValidator<ApproveEventCommand>
    {
        public ApproveEventCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CanBeEditedTill)
                .GreaterThan(DateTime.Now)
                .WithMessage("provided value should be greater than current time");
        }
    }
}