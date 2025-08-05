using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface ICardDetailsService : ICrudServiceWithoutUpdate<CardDetails, CardDetailsDto, CardDetailsCreateDto>
{
}