using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Specifications;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.GetApprovedEvents
{
    public class GetApprovedEventsQuery: IRequest<EventsListVm>
    {
        public class GetApprovedEventsQueryHandler : BaseRequestHandler<GetApprovedEventsQuery,EventsListVm>
        {
            public GetApprovedEventsQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventsListVm> Handle(GetApprovedEventsQuery request, CancellationToken cancellationToken)
            {
                var spec = new ApprovedEventsSpecification();
                var events = await EventRepo.ListBySpecAsync(spec, cancellationToken);

                var vm = new EventsListVm
                {
                    Events = events.Adapt<ICollection<EventDto>>()
                };

                return vm;
            }
        }
        
    }
}