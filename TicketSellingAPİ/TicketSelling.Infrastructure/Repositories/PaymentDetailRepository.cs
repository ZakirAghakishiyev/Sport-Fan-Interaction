using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class PaymentDetailRepository : EfCoreRepository<PaymentDetail>, IPaymentDetailRepository
{
    public PaymentDetailRepository(AppDbContext context) : base(context) { }
}