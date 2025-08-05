using TicketSelling.Application.Dtos.MatchSectorPrice;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IMatchSectorPriceDto:
    ICrudService<MatchSectorPrice,MatchSectorPriceDto, MatchSectorPriceCreateDto, MatchSectorPriceUpdateDto> { }