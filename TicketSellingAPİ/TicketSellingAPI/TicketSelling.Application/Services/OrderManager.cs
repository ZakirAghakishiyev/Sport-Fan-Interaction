using TicketSelling.Application.Dtos.Order;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class OrderManager(IRepository<Order> repository) 
    : CrudManagerWithoutUpdate<Order, OrderDto, OrderCreateDto>(repository)
    , IOrderService
{
}
