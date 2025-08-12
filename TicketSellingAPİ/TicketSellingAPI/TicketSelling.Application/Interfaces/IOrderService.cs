using TicketSelling.Application.Dtos.Order;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IOrderService : ICrudServiceWithoutUpdate<Order, OrderDto, OrderCreateDto> {}