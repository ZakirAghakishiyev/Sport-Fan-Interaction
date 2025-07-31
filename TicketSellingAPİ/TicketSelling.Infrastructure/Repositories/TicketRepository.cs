using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class TicketRepository : EfCoreRepository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context) { }
}
