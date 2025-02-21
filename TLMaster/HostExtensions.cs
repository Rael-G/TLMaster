using System.Globalization;
using System.Text;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TLMaster.Persistence.Contexts;

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

    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ??
            throw new Exception("Cors allowed origins are not defined in configuration.");
        services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));
    }

    public static void ConfigureDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TLMaster",
            });

            var xmlFile = "TLMaster.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]??
            throw new Exception("Jwt Secret is not defined in configuration."));
        services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RequireExpirationTime = true
                };
            })
            .AddDiscord(options =>
            {
                options.ClientId = configuration["Discord:ClientId"]?? 
                    throw new Exception("Discord client Id is not defined in configuration.");
                options.ClientSecret = configuration["Discord:ClientSecret"]?? 
                    throw new Exception("Discord client Secret is not defined in configuration.");
                options.CallbackPath = new PathString("/signin-discord");

                options.SaveTokens = true;

                options.ClaimActions.MapCustomJson("urn:discord:avatar:url", user =>
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                        user.GetString("id"),
                        user.GetString("avatar"),
                        user.GetString("avatar")!.StartsWith("a_") ? "gif" : "png"));
                
                options.Scope.Add("identify");
                options.Scope.Add("email");
            });

        services.AddAuthorization();
    }
}
