using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface ISeatService : ICrudService<Seat, SeatDto, SeatCreateDto, SeatUpdateDto> { }