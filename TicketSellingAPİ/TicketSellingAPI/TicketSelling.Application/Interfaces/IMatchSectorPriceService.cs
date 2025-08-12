using TicketSelling.Application.Dtos.MatchSectorPrice;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IMatchSectorPriceService:
    ICrudService<MatchSectorPrice,MatchSectorPriceDto, MatchSectorPriceCreateDto, MatchSectorPriceUpdateDto> { }