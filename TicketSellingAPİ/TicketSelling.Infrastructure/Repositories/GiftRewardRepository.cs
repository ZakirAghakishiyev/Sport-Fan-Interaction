using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class GiftRewardRepository : EfCoreRepository<GiftReward>, IGiftRewardRepository
{
    public GiftRewardRepository(AppDbContext context) : base(context) { }
}
