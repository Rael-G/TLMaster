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
        services.AddScoped<IActivityRepository, ActivityRepository>();
    }

    public static void SeedDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();

        Seed(context);
    }

    private static void Seed(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            var user1 = new User { Id = Guid.NewGuid(), UserName = "Knight001", Email = "knight001@example.com" };
            var user2 = new User { Id = Guid.NewGuid(), UserName = "Mage002", Email = "mage002@example.com" };
            var user3 = new User { Id = Guid.NewGuid(), UserName = "Archer003", Email = "archer003@example.com" };
            var user4 = new User { Id = Guid.NewGuid(), UserName = "Warrior004", Email = "warrior004@example.com" };

            var guild1 = new Guild { Id = Guid.NewGuid(), GuildMasterId = user1.Id };
            var guild2 = new Guild { Id = Guid.NewGuid(), GuildMasterId = user2.Id };
            var guild3 = new Guild { Id = Guid.NewGuid(), GuildMasterId = user3.Id };

            var char1 = new Character { Id = Guid.NewGuid(), Name = "Sir Arthur", GuildId = guild1.Id, UserId = user1.Id };
            var char2 = new Character { Id = Guid.NewGuid(), Name = "Elder Merlin", GuildId = guild2.Id, UserId = user2.Id };
            var char3 = new Character { Id = Guid.NewGuid(), Name = "Robin Hood", GuildId = guild3.Id, UserId = user3.Id };
            var char4 = new Character { Id = Guid.NewGuid(), Name = "Thor", GuildId = guild1.Id, UserId = user4.Id };

            var item1 = new Item { Id = Guid.NewGuid(), Name = "Excalibur", OwnerId = char1.Id };
            var item2 = new Item { Id = Guid.NewGuid(), Name = "Ancient Staff", OwnerId = char2.Id };
            var item3 = new Item { Id = Guid.NewGuid(), Name = "Shadow Bow", OwnerId = char3.Id };
            var item4 = new Item { Id = Guid.NewGuid(), Name = "Mjolnir", OwnerId = char4.Id };

            var auction1 = new Auction { Id = Guid.NewGuid(), ItemId = item3.Id, GuildId = guild3.Id, WinnerId = null };
            var auction2 = new Auction { Id = Guid.NewGuid(), ItemId = item4.Id, GuildId = guild1.Id, WinnerId = null };

            var bid1 = new Bid { Id = Guid.NewGuid(), AuctionId = auction1.Id, BidderId = char1.Id, Amount = 100 };
            var bid2 = new Bid { Id = Guid.NewGuid(), AuctionId = auction1.Id, BidderId = char2.Id, Amount = 150 };
            var bid3 = new Bid { Id = Guid.NewGuid(), AuctionId = auction2.Id, BidderId = char3.Id, Amount = 200 };

            var party1 = new Party { Id = Guid.NewGuid(), GuildId = guild1.Id };
            var party2 = new Party { Id = Guid.NewGuid(), GuildId = guild3.Id };

            party1.Characters = new List<Character> { char1, char4 };
            party2.Characters = new List<Character> { char2, char3 };

            context.Users.AddRange(user1, user2, user3, user4);
            context.Guilds.AddRange(guild1, guild2, guild3);
            context.Characters.AddRange(char1, char2, char3, char4);
            context.Items.AddRange(item1, item2, item3, item4);
            context.Auctions.AddRange(auction1, auction2);
            context.Bids.AddRange(bid1, bid2, bid3);
            context.Parties.AddRange(party1, party2);
            
            context.SaveChanges();
        }
    }
}