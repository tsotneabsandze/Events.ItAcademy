using API.Extensions.Di;
using API.Extensions.Swagger;
using API.Middlewares.ExceptionHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using FluentValidation.AspNetCore;
using MEDIATOR.Account.Queries.GetUserDetails;
using MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsByEmail;
using MEDIATOR.Common.Models;


namespace API
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
            services.AddControllers(opt =>
                {
                    opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                    opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                    opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                    opt.Filters.Add(new ConsumesAttribute("application/json"));
                    opt.Filters.Add(new ProducesAttribute("application/json"));

                    opt.ReturnHttpNotAcceptable = true;
                }
            ).AddNewtonsoftJson()
                .AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssemblyContaining<AuthResult>();
            });

            services.AddServices(_config);
            services.AddAuth(_config);

            services.AddVersioning();
            services.AddVersionAwareApiExplorer();
            services.AddSwaggerDocumentation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider descProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseSwaggerDocumentation(descProvider);

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}