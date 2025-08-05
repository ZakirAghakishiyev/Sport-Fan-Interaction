using TicketSelling.Application.Dtos.Payment;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IPaymentService : ICrudService<Payment, PaymentDto, PaymentCreateDto, PaymentUpdateDto> { }