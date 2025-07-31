using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class MatchRepository : EfCoreRepository<Match>, IMatchRepository
{
    public MatchRepository(AppDbContext context) : base(context) { }
}
