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
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Winner)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            // Bid
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Bidder)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            // Character
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Guild)
                .WithMany(g => g.Characters)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.Owner)
                .OnDelete(DeleteBehavior.SetNull);

            // Guild
            modelBuilder.Entity<Guild>()
                .HasOne(g => g.GuildMaster)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Guild>()
                .HasMany(g => g.Staff)
                .WithMany();

            modelBuilder.Entity<Guild>()
                .HasMany(g => g.Auctions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Guild>()
                .HasMany(g => g.Parties)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Party
            modelBuilder.Entity<Party>()
                .HasMany(p => p.Characters)
                .WithMany();

            // User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Characters)
                .WithOne();
        }
}
