using TicketSelling.Application.Dtos.PaymentDetail;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class PaymentDetailManager(IRepository<PaymentDetail> repository) 
    : CrudManagerWithoutUpdate<PaymentDetail, PaymentDetailDto, PaymentDetailCreateDto>(repository)
    , IPaymentDetailService
{
}
