using TicketSelling.Application.Dtos.Payment;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class PaymentManager : CrudManager<Payment, PaymentDto, PaymentCreateDto, PaymentUpdateDto>, IPaymentService
{
    public PaymentManager(IRepository<Payment> repository) : base(repository)
    {
    }
}
