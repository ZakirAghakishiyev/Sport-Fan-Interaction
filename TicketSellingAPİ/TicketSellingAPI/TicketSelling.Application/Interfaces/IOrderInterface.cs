using TicketSelling.Application.Dtos.Order;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IOrderInterface : ICrudServiceWithoutUpdate<Order, OrderDto, OrderCreateDto> {}