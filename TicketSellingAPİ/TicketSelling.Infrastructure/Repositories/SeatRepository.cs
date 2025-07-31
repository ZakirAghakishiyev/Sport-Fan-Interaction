using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class SeatRepository : EfCoreRepository<Seat>, ISeatRepository
{
    public SeatRepository(AppDbContext context) : base(context) { }
}
