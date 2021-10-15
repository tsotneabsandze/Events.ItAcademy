using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Specifications;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList
{
    public class GetEventsListQuery : IRequest<PaginatedResult<EventsListVm>>
    {
        public SpecParams SpecParams { get; set; }

        public class GetEventsListQueryHandler : BaseRequestHandler<GetEventsListQuery, PaginatedResult<EventsListVm>>
        {
            public GetEventsListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<PaginatedResult<EventsListVm>> Handle(GetEventsListQuery request,
                CancellationToken cancellationToken)
            {
                var spec = new OrderedEventsListSpecification(request.SpecParams);
                var events = await EventRepo.ListBySpecAsync(spec, cancellationToken);

                var content = new EventsListVm
                {
                    Events = events.Adapt<ICollection<EventDto>>()
                };

                var paginatedResult = new PaginatedResult<EventsListVm>
                {
                    Content = content,
                    Count = await EventRepo.CountAsync(cancellationToken),
                    PageIndex = request.SpecParams.PageIndex,
                    PageSize = request.SpecParams.PageSize
                };

                return paginatedResult;
            }
        }
    }
}