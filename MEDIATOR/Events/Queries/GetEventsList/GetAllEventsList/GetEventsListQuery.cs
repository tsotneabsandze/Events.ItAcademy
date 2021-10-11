using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList
{
    public class GetEventsListQuery : IRequest<EventsListVm>
    {
        public class GetEventsListQueryHandler : BaseRequestHandler<GetEventsListQuery, EventsListVm>
        {
            public GetEventsListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventsListVm> Handle(GetEventsListQuery request,
                CancellationToken cancellationToken)
            {
                var events = await EventRepo.ListAllAsync(cancellationToken);

                var vm = new EventsListVm
                {
                    Events = events.Adapt<ICollection<EventDto>>()
                };

                return vm;
            }
        }
    }
}