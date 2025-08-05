using TicketSelling.Application.Dtos.OrderItem;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IOrderItemService : ICrudServiceWithoutUpdate<OrderItem, OrderItemDto, OrderItemCreateDto> { }
