using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;

namespace OrderService.Presentation.Extensions;

public static class WebApplicationExtension
{

    public static void Configure(this WebApplication app)
    {
        app.UseHealthChecks("/health",
           new HealthCheckOptions
           {
               ResponseWriter = async (context, report) =>
               {
                   var result = new
                   {
                       status = report.Status.ToString(),
                       errors = report.Entries.Select(e => new
                       {
                           key = e.Key,
                           value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                       })
                   };
                   context.Response.ContentType = MediaTypeNames.Application.Json;
                   await context.Response.WriteAsync(JsonSerializer.Serialize(result));
               }
           });

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseMiddleware<MiddlewareException>();

        app.MapControllers();

        app.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            return next();
        });
    }

}
