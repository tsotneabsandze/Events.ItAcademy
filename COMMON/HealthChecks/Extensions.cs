using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.HealthChecks
{
    public static class Extensions
    {
        // public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        // {
        //     app.UseEndpoints(endpoints =>
        //     {
        //         endpoints.MapHealthChecks("/Health", new HealthCheckOptions()
        //         {
        //             ResponseWriter = async (ctx, report) =>
        //             {
        //                 ctx.Response.ContentType = "application/json";
        //                 var resp = new HealthCheckResponse
        //                 {
        //                     Status = report.Status.ToString(),
        //                     Checks = report.Entries.Select(x => new IndividualHealthCheckResponse()
        //                     {
        //                         Component = x.Key,
        //                         Status = x.Value.Status.ToString(),
        //                         Description = x.Value.Description
        //                     }),
        //                     Duration = report.TotalDuration
        //                 };
        //                 await ctx.Response.WriteAsync(JsonConvert.SerializeObject(resp));
        //             }
        //         });
        //     });
        //     return app;
        // }
        public static IApplicationBuilder CheckHealths(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResult
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new IndividualHealthCheckResult
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });
            return app;
        }
    }
}