using System;
using Common.ActionFilters;
using Common.Constants;
using Common.HealthChecks;
using Common.Models.Login;
using Common.Services.Abstractions;
using Common.Services.Implementations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EVENTS.MVC
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options => { options.EnableEndpointRouting = false; })
                .AddFluentValidation(c =>
                {
                    c.RegisterValidatorsFromAssemblyContaining(typeof(LoginVm));
                    c.RegisterValidatorsFromAssemblyContaining(typeof(Startup));
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(4);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddHealthChecks()
                .AddUrlGroup(new Uri($"{ApiConstants.BaseApiUrl}/Events"), name: "EventsApi");;
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(ISessionService), typeof(SessionService));
            services.AddScoped<CheckTokenFilter>();
            services.AddScoped<IImageService, ImageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            app.CheckHealths();
        }
    }
}