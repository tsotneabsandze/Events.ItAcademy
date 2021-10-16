using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList
{
    public class GetAllEventsListQuery : IRequest<IReadOnlyList<PartialEventDto>>
    {
        public class
            GetAllEventsListQueryHandler : BaseRequestHandler<GetAllEventsListQuery, IReadOnlyList<PartialEventDto>>
        {
            public GetAllEventsListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<IReadOnlyList<PartialEventDto>> Handle(GetAllEventsListQuery request,
                CancellationToken cancellationToken)
            {
                var events = await EventRepo.ListAllAsync(cancellationToken);

                return events.Adapt<IReadOnlyList<PartialEventDto>>();
            }
        }
    }
}