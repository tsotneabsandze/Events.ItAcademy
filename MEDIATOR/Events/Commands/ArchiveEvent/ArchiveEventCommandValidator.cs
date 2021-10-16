using System.Data;
using FluentValidation;

namespace MEDIATOR.Events.Commands.ArchiveEvent
{
    public class ArchiveEventCommandValidator : AbstractValidator<ArchiveEventCommand>
    {
        public ArchiveEventCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}