using TicketSelling.Application.Dtos.PaymentDetail;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IPaymentDetailService 
    : ICrudServiceWithoutUpdate<PaymentDetail, PaymentDetailDto, PaymentDetailCreateDto> { }