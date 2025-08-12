using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class SeatManager(IRepository<Seat> repository) 
    : CrudManager<Seat, SeatDto, SeatCreateDto, SeatUpdateDto>(repository)
    , ISeatService
{
}
