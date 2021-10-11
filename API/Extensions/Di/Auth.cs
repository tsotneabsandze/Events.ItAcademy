using System.Text;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using INFRASTRUCTURE.Identity;
using INFRASTRUCTURE.Identity.Models;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using INFRASTRUCTURE.Identity.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            var key = Encoding.ASCII.GetBytes(config.GetSection("JwtConfig").GetSection("Secret").Value);
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // ValidIssuer = "localhost",
                        // ValidAudience = "localhost"
                    };
                }
            );

            services.AddAuthorization(
                options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder(
                            JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
                    
                    
                    options.AddPolicy("RequireAdminRole",
                        policy => policy.RequireRole("Admin"));
                    options.AddPolicy("RequireBasicRole",
                        policy => policy.RequireRole("Basic"));
                }
            );
            
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}