using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class SectorRepository : EfCoreRepository<Sector>, ISectorRepository
{
    public SectorRepository(AppDbContext context) : base(context) { }
}
