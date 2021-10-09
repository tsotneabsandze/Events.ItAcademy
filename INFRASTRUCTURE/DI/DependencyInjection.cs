using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INFRASTRUCTURE.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericEfRepository< >));

            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtConfig>(config => configuration.GetSection("JwtConfig"));
            
            return services;
        }
    }
}