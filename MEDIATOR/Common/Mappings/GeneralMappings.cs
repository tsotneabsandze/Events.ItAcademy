using CORE.Entities;
using INFRASTRUCTURE.Identity.Models;
using Mapster;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Account.Queries.GetUsersList;
using MEDIATOR.Common.Models;
using MEDIATOR.Events.Commands.CreateEvent;
using MEDIATOR.Events.Queries.GetEventsList.GetAllEventsList;
using Microsoft.Extensions.DependencyInjection;

namespace MEDIATOR.Common.Mappings
{
    public static class GeneralMappings
    {
        public static void AddMappings(this IServiceCollection services)
        {
            TypeAdapterConfig<ApplicationUser, UserDetailsVm>
                .NewConfig();

            TypeAdapterConfig<ApplicationUser, UserLookupDto>
                .NewConfig();

            TypeAdapterConfig<CreateEventCommand, Event>
                .NewConfig();

            TypeAdapterConfig<Event, EventDto>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<Event, PartialEventDto>
                .NewConfig()
                .TwoWays();
        }
    }
}