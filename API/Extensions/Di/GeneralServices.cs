using MediatR;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using INFRASTRUCTURE.Identity.Options;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using INFRASTRUCTURE.Identity.Services.Implementations;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Common.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace API.Extensions.Di
{
    public static class GeneralServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericEfRepository<>));
            services.AddScoped<ITokenService,TokenService>();

            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            

            services.Configure<JwtConfig>(config.GetSection("JwtConfig"));

            services.AddMediatR(typeof(GetUserDetailsQuery).Assembly);
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(RequestValidationBehaviour<,>));

            return services;
        }
    }
}
