using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;
using TLMaster.Persistance.Repositories;

namespace TLMaster.Persistance;

public static class PersistanceExtensions
{
    public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<IGuildRepository, GuildRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
