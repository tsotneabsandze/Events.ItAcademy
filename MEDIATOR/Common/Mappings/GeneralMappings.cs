using INFRASTRUCTURE.Identity.Models;
using Mapster;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Account.Queries.GetUsersList;
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
            

        }
    }
}