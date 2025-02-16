using System;
using TLMaster.Application.Interfaces;
using TLMaster.Application.Services;

namespace TLMaster.Application;

public static class ApplicationExtensions
    {
        /// <summary>
        /// Configures application-related services.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Registers the AutoMapper service
            services.AddAutoMapper(typeof(Mapper));

            // Registers the PostService and CommentService services with scoped lifetime.
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IGuildService, GuildService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IPartyService, PartyService>();
        }
    }
