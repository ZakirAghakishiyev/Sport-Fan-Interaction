using TicketSelling.Application.Dtos.Match;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IMatchService : ICrudService<Match, MatchDto, MatchCreateDto, MatchUpdateDto> { }