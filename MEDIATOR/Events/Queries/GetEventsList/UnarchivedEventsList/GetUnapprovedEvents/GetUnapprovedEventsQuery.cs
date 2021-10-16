using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Specifications;
using CORE.Specifications.EventSpecifications.Unarchived;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetUnapprovedEvents
{
    public class GetUnapprovedEventsQuery: IRequest<EventsListVm>
    {
        public class GetUnapprovedEventsQueryHandler : BaseRequestHandler<GetUnapprovedEventsQuery,EventsListVm>
        {
            public GetUnapprovedEventsQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventsListVm> Handle(GetUnapprovedEventsQuery request, CancellationToken cancellationToken)
            {
                var spec = new UnapprovedEventsSpecification();
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