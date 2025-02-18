using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;
using TLMaster.Persistence.Repositories;

namespace TLMaster.Persistence;

public static class PersistenceExtensions
{
    /// <summary>
    /// Configures persistence-related services such as database context and repositories.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <param name="configuration">The configuration settings.</param>
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

        services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<IGuildRepository, GuildRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
