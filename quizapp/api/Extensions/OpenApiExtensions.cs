using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiExtension(this IServiceCollection service)
    {
        // Add Swagger Gen
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quiz App API",
                    Version = "v1",
                    Description = "A quiz application API",
                    Contact = new OpenApiContact
                    {
                        Name = "Quiz App Team",
                        Email = "support@quizapp.com"
                    }
                });

                // Include XML comments if they exist
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                // avoid duplicate schemaId
                options.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            }
        );
        // Add Api Versioning
        service.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        });

        service.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });



        return service;
    }

    public static WebApplication MapOpenApiExtension(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz App API v1");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}
