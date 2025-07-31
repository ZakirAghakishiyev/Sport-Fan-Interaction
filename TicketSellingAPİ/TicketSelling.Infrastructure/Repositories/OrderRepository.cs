using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class OrderRepository : EfCoreRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }
}
