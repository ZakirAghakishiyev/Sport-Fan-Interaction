using TicketSelling.Application.Dtos.Ticket;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface ITicketService : ICrudServiceWithoutUpdate<Ticket, TicketDto, TicketCreateDto> { }