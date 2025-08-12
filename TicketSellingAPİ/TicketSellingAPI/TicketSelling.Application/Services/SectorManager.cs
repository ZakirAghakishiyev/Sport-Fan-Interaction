using TicketSelling.Application.Dtos.Sector;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class SectorManager(IRepository<Sector> repository) 
    : CrudManager<Sector, SectorDto, SectorCreateDto, SectorUpdateDto>(repository)
    , ISectorService
{
}
