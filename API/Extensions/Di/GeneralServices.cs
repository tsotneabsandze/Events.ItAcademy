using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity.Options;
using INFRASTRUCTURE.Identity.Services;
using Microsoft.EntityFrameworkCore;
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
            

            services.Configure<JwtConfig>(c => config.GetSection("JwtConfig"));

            return services;
        }
    }
}
