using TicketSelling.Application.Dtos.Stadium;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IStadiumService : ICrudService<Stadium, StadiumDto, StadiumCreateDto, StadiumUpdateDto> { }