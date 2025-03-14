using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TLMaster.Application.Enums;
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

        services.AddIdentity<User, ApplicationRole>()
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
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IBalanceRepository, BalanceRepository>();
    }

    public static async Task ConfigureRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        foreach (var role in ApplicationRoles.Roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }
    }

    public static void SeedDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var configuration = app.Configuration;

        Seed(context, userManager, configuration);
    }

    private static void Seed(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
    {
        if (!context.Users.Any())
        {
            CreateAdmin(userManager, configuration);
            context.SaveChanges();
        }
    }

    private static void CreateAdmin(UserManager<User> userManager, IConfiguration configuration)
    {
        var adminEmail = configuration["AdminEmail"] ?? string.Empty;
        var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(adminUser).Result;
            if (!result.Succeeded)
            {
                throw new Exception("Falha ao criar o usuário admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var externalLogin = new UserLoginInfo("Discord", adminEmail, "Discord");
            userManager.AddLoginAsync(adminUser, externalLogin).Wait();
        }
    }
}