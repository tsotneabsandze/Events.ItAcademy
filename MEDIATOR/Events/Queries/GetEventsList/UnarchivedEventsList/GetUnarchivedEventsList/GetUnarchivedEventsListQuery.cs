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

namespace MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetUnarchivedEventsList
{
    public class GetUnarchivedEventsListQuery : IRequest<PaginatedResult<EventsListVm>>
    {
        public SpecParams SpecParams { get; set; }

        public class GetEventsListQueryHandler : BaseRequestHandler<GetUnarchivedEventsListQuery, PaginatedResult<EventsListVm>>
        {
            public GetEventsListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<PaginatedResult<EventsListVm>> Handle(GetUnarchivedEventsListQuery request,
                CancellationToken cancellationToken)
            {
                var spec = new UnarchivedOrderedEventsListSpecification(request.SpecParams);
                var countSpec = new ValidEventsCountSpecification();
                
                var events = await EventRepo.ListBySpecAsync(spec, cancellationToken);

                var content = new EventsListVm
                {
                    Events = events.Adapt<ICollection<EventDto>>()
                };

                var paginatedResult = new PaginatedResult<EventsListVm>
                {
                    Content = content,
                    Count = await EventRepo.CountAsync(countSpec,cancellationToken),
                    PageIndex = request.SpecParams.PageIndex,
                    PageSize = request.SpecParams.PageSize
                };

                return paginatedResult;
            }
        }
    }
}