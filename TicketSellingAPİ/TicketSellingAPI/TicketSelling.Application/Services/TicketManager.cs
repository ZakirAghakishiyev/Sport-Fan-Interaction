using TicketSelling.Application.Dtos.Ticket;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class TicketManager(IRepository<Ticket> repository) 
    : CrudManagerWithoutUpdate<Ticket, TicketDto, TicketCreateDto>(repository)
    , ITicketService
{
}
