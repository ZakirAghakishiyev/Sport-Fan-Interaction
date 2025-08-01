using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class CardDetailsRepository : EfCoreRepository<CardDetails>, ICardDetailsRepository
{
    public CardDetailsRepository(AppDbContext context) : base(context) { }
}