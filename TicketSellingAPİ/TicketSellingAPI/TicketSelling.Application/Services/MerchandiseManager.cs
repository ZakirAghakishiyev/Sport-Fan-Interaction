using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;
using Elastic.Clients.Elasticsearch;

namespace TicketSelling.Application.Services;

public class MerchandiseManager(IRepository<Merchandise> repository) 
    : CrudManager<Merchandise, MerchandiseDto, MerchandiseCreateDto, MerchandiseUpdateDto>(repository)
    , IMerchandiseService
{
}
