using MediatR;
using MEDIATOR;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using INFRASTRUCTURE.Identity.Options;
using INFRASTRUCTURE.Identity.Services;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using INFRASTRUCTURE.Identity.Services.Implementations;
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
            

            services.Configure<JwtConfig>(c => config.GetSection("JwtConfig")
                .Bind("JwtConfig"));

            services.AddMediatR(typeof(class1).Assembly);
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
