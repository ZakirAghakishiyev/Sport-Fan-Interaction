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
    DbSet<User> Users { get; set; }
}
