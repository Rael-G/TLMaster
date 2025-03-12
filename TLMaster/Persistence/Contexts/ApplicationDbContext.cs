using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;

namespace TLMaster.Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, ApplicationRole, Guid>(options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Party> Activities { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Auction
        modelBuilder.Entity<Auction>()
            .HasOne(a => a.Item)
            .WithOne(i => i.Auction)
            .HasForeignKey<Auction>(a => a.ItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Auction>()
            .HasMany(a => a.Bids)
            .WithOne(b => b.Auction)
            .HasForeignKey(b => b.AuctionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Auction>()
            .HasOne(a => a.Winner)
            .WithMany()
            .HasForeignKey(a => a.WinnerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Bid
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Bidder)
            .WithMany(c => c.Bids)
            .HasForeignKey(b => b.BidderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Character
        modelBuilder.Entity<Character>()
            .HasOne(c => c.Guild)
            .WithMany(g => g.Members)
            .HasForeignKey(c => c.GuildId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Itens)
            .WithOne(i => i.Owner)
            .HasForeignKey(i => i.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Applications)
            .WithMany(g => g.Applicants);

        // Guild
        modelBuilder.Entity<Guild>()
            .HasOne(g => g.GuildMaster)
            .WithMany(u => u.OwnedGuilds)
            .HasForeignKey(g => g.GuildMasterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Staff)
            .WithMany(u => u.StaffGuilds);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Members)
            .WithOne(c => c.Guild)
            .HasForeignKey(c => c.GuildId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Auctions)
            .WithOne(a => a.Guild)
            .HasForeignKey(a => a.GuildId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Parties)
            .WithOne(a => a.Guild)
            .HasForeignKey(p => p.GuildId)
            .OnDelete(DeleteBehavior.Cascade);

        // Party
        modelBuilder.Entity<Party>()
            .HasMany(p => p.Characters)
            .WithMany(c => c.Parties);

        // User
        modelBuilder.Entity<User>()
            .HasMany(u => u.Characters)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        // Activity
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.Guild)
            .WithMany(g => g.Activities)
            .HasForeignKey(a => a.GuildId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Activity>()
            .HasMany(a => a.Participants)
            .WithMany(c => c.Activities);

        // RefreshToken
        modelBuilder.Entity<RefreshToken>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Item
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Guild)
            .WithMany(g => g.Items)
            .HasForeignKey(i => i.GuildId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);
    }

}
