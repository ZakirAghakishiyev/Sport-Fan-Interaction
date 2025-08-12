using TicketSelling.Application.Dtos.Stadium;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class StadiumManager(IRepository<Stadium> repository) 
    : CrudManager<Stadium, StadiumDto, StadiumCreateDto, StadiumUpdateDto>(repository)
    , IStadiumService
{
}
