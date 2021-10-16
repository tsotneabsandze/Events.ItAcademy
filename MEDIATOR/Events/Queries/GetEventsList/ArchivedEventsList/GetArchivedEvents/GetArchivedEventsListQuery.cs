using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Specifications;
using CORE.Specifications.EventSpecifications.Archived;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.ArchivedEventsList.GetArchivedEvents
{
    public class GetArchivedEventsListQuery :  IRequest<EventsListVm>
    {
        public class GetArchivedEventsListQueryHandler : BaseRequestHandler<GetArchivedEventsListQuery,EventsListVm>
        {
            public GetArchivedEventsListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventsListVm> Handle(GetArchivedEventsListQuery request, CancellationToken cancellationToken)
            {
                var spec = new ArchivedOrderedEventsListSpecification();
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