using Microsoft.EntityFrameworkCore;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    DbSet<Ticket> Tickets { get; set; }
    DbSet<Seat> Seats { get; set; }
    DbSet<GiftReward> GiftRewards { get; set; }
    DbSet<Match> Matches { get; set; }
    DbSet<MatchSectorPrice> MatchesSectorPrice { get; set; }
    DbSet<Merchandise> Merchandises { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Sector> Sectors{ get; set; }
    DbSet<Stadium> Stadiums { get; set; }
    DbSet<AppUser> Users { get; set; }

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
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
