using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Events.Commands.ApproveEvent
{
    public class ApproveEventCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime? CanBeEditedTill { get; set; }

        public class ApproveEventCommandHandler : BaseRequestHandler<ApproveEventCommand, Unit>
        {
            public ApproveEventCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(ApproveEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await EventRepo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

                if (entity is null)
                    throw new ResourceNotFoundException($"resource with id {request.Id} was not found");

                var date = request.CanBeEditedTill;
                if (date != default && date > entity.Starts)
                    throw new InvalidDateException("invalid date");

                entity.CanBeEditedTill = request.CanBeEditedTill;
                entity.IsApproved = true;

                await EventRepo.UpdateAsync(entity, cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}