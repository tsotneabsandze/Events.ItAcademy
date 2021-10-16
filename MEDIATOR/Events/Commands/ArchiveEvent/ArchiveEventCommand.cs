using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Events.Commands.ArchiveEvent
{
    public class ArchiveEventCommand : IRequest
    {
        public int Id { get; set; }

        public class ArchiveEventCommandHandler : BaseRequestHandler<ArchiveEventCommand,Unit>
        {
            public ArchiveEventCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(ArchiveEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await EventRepo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

                if (entity is null)
                    throw new ResourceNotFoundException($"resource with id {request.Id} was not found");

                entity.IsArchived = true;
                
                await EventRepo.UpdateAsync(entity, cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}