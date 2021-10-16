using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using CORE.Specifications;
using CORE.Specifications.EventSpecifications.Unarchived;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Events.Queries.GetEventsList.UnarchivedEventsList.GetSpecificUsersEvents
{
    public class GetSpecificUserEventsQuery : IRequest<EventsListVm>
    {
        public string Id { get; set; }

        public class GetSpecificUserEventsQueryHandler : BaseRequestHandler<GetSpecificUserEventsQuery, EventsListVm>
        {
            public GetSpecificUserEventsQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<EventsListVm> Handle(GetSpecificUserEventsQuery request,
                CancellationToken cancellationToken)
            {
                var user = await AccountService.GetUserByIdAsync(request.Id);
                if (user is null)
                    throw new ResourceNotFoundException($"user with id {request.Id} was not found");

                var spec = new GetUserEventsByUserIdSpecification(request.Id);
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