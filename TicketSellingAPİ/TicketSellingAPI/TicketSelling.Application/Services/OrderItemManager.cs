using TicketSelling.Application.Dtos.OrderItem;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class OrderItemManager(IRepository<OrderItem> repository) 
    : CrudManagerWithoutUpdate<OrderItem, OrderItemDto, OrderItemCreateDto>(repository)
    , IOrderItemService
{
}
