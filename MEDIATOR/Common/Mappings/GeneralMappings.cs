using INFRASTRUCTURE.Identity;
using INFRASTRUCTURE.Identity.Models;
using Mapster;
using MEDIATOR.Account.Queries.GetUserDetails;
using Microsoft.Extensions.DependencyInjection;

namespace MEDIATOR.Common.Mappings
{
    public static class GeneralMappings
    {
        public static void AddMappings(this IServiceCollection services)
        {
            TypeAdapterConfig<ApplicationUser, UserDetailsVm>
                .NewConfig();

        }
    }
}