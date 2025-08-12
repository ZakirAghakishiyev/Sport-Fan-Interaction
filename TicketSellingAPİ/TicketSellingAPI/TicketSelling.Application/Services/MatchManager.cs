using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class MatchManager(IRepository<Match> repository) 
    : CrudManager<Match, MatchDto, MatchCreateDto, MatchUpdateDto>(repository)
    , IMatchService
{
}
