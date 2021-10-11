using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<EventDto>
    {
        public int Id { get; set; }
        
        public class GetEventDetailQueryHandler : BaseRequestHandler<GetEventDetailQuery,EventDto>
        {
            public GetEventDetailQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventDto> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await EventRepo.GetByIdAsync(request.Id, cancellationToken);

                if (entity is null)
                    throw new ResourceNotFoundException($"resource with id {request.Id} was not found");

                return entity.Adapt<EventDto>();
            }
        }
    }
}