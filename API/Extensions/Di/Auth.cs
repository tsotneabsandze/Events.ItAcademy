using System.Text;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions.Di
{
    public static class Auth
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var key = Encoding.ASCII.GetBytes(config.Get<JwtConfig>().Secret);
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme
                    = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme
                    = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "localhost",
                        ValidAudience = "localhost"
                    };
                }
            );

            return services;
        }
    }
}