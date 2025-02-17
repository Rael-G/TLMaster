using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;

namespace TLMaster.Persistance.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<User> Users { get; set; }

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
            .WithMany(g => g.Characters)
            .HasForeignKey(c => c.GuildId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Itens)
            .WithOne(i => i.Owner)
            .HasForeignKey(i => i.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Guild
        modelBuilder.Entity<Guild>()
            .HasOne(g => g.GuildMaster)
            .WithMany(u => u.OwnedGuilds)
            .HasForeignKey(g => g.GuildMasterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Staff)
            .WithMany(u => u.StaffGuilds);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Characters)
            .WithOne(c => c.Guild)
            .HasForeignKey(c => c.GuildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Auctions)
            .WithOne(a => a.Guild)
            .HasForeignKey(a => a.GuildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Guild>()
            .HasMany(g => g.Parties)
            .WithOne(a => a.Guild)
            .HasForeignKey(p => p.GuildId)
            .OnDelete(DeleteBehavior.Cascade);

        // Party
        modelBuilder.Entity<Party>()
            .HasMany(p => p.Characters)
            .WithMany();

        // User
        modelBuilder.Entity<User>()
            .HasMany(u => u.Characters)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .IsRequired();
    }

}
