using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class StadiumRepository : EfCoreRepository<Stadium>, IStadiumRepository
{
    public StadiumRepository(AppDbContext context) : base(context) { }
}
