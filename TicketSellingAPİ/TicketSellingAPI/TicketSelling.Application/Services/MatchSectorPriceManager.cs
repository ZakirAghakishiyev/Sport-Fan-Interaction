using TicketSelling.Application.Dtos.MatchSectorPrice;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class MatchSectorPriceManager(IRepository<MatchSectorPrice> repository) 
    : CrudManager<MatchSectorPrice, MatchSectorPriceDto, MatchSectorPriceCreateDto, MatchSectorPriceUpdateDto>(repository)
    , IMatchSectorPriceService
{
}
