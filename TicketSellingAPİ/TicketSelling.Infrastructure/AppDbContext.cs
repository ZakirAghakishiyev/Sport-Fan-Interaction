using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<GiftReward> GiftRewards { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchSectorPrice> MatchesSectorPrice { get; set; }
    public DbSet<Merchandise> Merchandises { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MatchSectorPrice>()
            .HasOne(msp => msp.Match)
            .WithMany(m => m.SectorPrices)
            .HasForeignKey(msp => msp.MatchId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MatchSectorPrice>()
            .HasOne(msp => msp.Sector)
            .WithMany()
            .HasForeignKey(msp => msp.SectorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithMany()
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets) 
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
