using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TLMaster.Persistance.Contexts;

namespace TLMaster;

public static class HostExtensions
{
    public static void InitializeDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TLMaster",
            });

            var xmlFile = "TLMaster.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
}
