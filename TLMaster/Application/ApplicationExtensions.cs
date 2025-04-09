using System.Text.Json;
using Quartz;
using Quartz.Impl.AdoJobStore;
using TLMaster.Application.Interfaces;
using TLMaster.Application.Mappings;
using TLMaster.Application.Services;

namespace TLMaster.Application;

public static class ApplicationExtensions
{
    /// <summary>
    /// Configures application-related services.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    public static void ConfigureApplication(this IServiceCollection services)
    {
        // Registers the AutoMapper service
        services.AddAutoMapper(typeof(DomainToDto));

        // Registers the PostService and CommentService services with scoped lifetime.
        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<ICharacterService, CharacterService>();
        services.AddScoped<IGuildService, GuildService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IPartyService, PartyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBalanceService, BalanceService>();

        // Registers Quartz to have jobs
        services.AddQuartz( q => q
            .UsePersistentStore(c => 
            {
                c.UseNewtonsoftJsonSerializer();
                c.UsePostgres(o => 
                {
                    o.ConnectionStringName = "ConnectionString";
                    o.UseDriverDelegate<PostgreSQLDelegate>();
                    o.TablePrefix = "quartz.qrtz_";
                });
            })
        );
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}
