using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IMerchandiseService : ICrudService<Merchandise, MerchandiseDto, MerchandiseCreateDto, MerchandiseUpdateDto> { }