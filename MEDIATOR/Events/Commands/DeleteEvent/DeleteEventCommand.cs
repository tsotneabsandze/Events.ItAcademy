using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Exceptions;
using CORE.Interfaces;
using MEDIATOR.Common.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Antiforgery.Internal;

namespace MEDIATOR.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public int Id { get; set; }
        
        public class DeleteEventCommandHandler:BaseRequestHandler<DeleteEventCommand,Unit>
        {
            public DeleteEventCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await EventRepo.GetByIdAsync(request.Id, cancellationToken);
                
                if (entity is null)
                    throw new ResourceNotFoundException($"event with id {request.Id} was not found");
                
                await EventRepo.DeleteAsync(entity, cancellationToken: cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}