using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class MatchSectorPriceRepository : EfCoreRepository<MatchSectorPrice>, IMatchSectorPriceRepository
{
    public MatchSectorPriceRepository(AppDbContext context) : base(context) { }
}
