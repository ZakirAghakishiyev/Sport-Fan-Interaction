using TicketSelling.Application.Dtos.Sector;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface ISectorService : ICrudService<Sector, SectorDto, SectorCreateDto, SectorUpdateDto> { }