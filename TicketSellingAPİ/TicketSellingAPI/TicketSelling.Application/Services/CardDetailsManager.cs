using AutoMapper;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class CardDetailsManager(IMapper _mapper, IRepository<CardDetails> _repository) 
    : CrudManagerWithoutUpdate<CardDetails, CardDetailsDto, CardDetailsCreateDto>(_repository)
    , ICardDetailsService
{
    public async override Task<CardDetailsDto> AddAsync(CardDetailsCreateDto createDto)
    {
        CardDetails entity = _mapper.Map<CardDetails>(createDto);
        entity.Last4Digits = int.Parse(createDto.All16Digits[^4..]);

        var res = await _repository.AddAsync(entity);
        return _mapper.Map<CardDetailsDto>(res);
    }
}
