using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class CardDetailsManager(IRepository<CardDetails> repository) 
    : CrudManagerWithoutUpdate<CardDetails, CardDetailsDto, CardDetailsCreateDto>(repository)
    , ICardDetailsService
{
}
