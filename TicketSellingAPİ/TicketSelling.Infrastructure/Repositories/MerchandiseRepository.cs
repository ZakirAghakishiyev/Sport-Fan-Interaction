using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class MerchandiseRepository : EfCoreRepository<Merchandise>, IMerchandiseRepository
{
    public MerchandiseRepository(AppDbContext context) : base(context) { }
}
