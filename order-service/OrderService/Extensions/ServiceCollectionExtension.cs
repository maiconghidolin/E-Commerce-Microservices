using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;

namespace OrderService.Presentation.Extensions;

public static class ServiceCollectionExtension
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Inform the JWT token",
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        services.AddHealthChecks();

        //services.AddValidatorsFromAssembly(Assembly.Load("OrderService.Domain"));

        //services.AddAuthentication(a =>
        //{
        //    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //   .AddJwtBearer(b => b.TokenValidationParameters = new TokenValidationParameters
        //   {
        //       ValidateIssuerSigningKey = true,
        //       ValidateIssuer = true,
        //       ValidateAudience = true,
        //       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecurityKey-ExampleHexagonalAPI-256bits")),
        //       ValidAudiences = new[] { "http://localhost" },
        //       ValidIssuers = new[] { "Example" },
        //       ClockSkew = TimeSpan.Zero
        //   });

        services.AddMvc(options =>
        {
            var noContentFormatter = options.OutputFormatters.OfType<HttpNoContentOutputFormatter>().FirstOrDefault();
            if (noContentFormatter != null)
            {
                noContentFormatter.TreatNullValueAsNoContent = false;
            }
        });

        services.AddHttpClient();
    }
}
