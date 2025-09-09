using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Application.Dtos.Stadium;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class StadiumManager(IMapper _mapper,IRepository<Stadium> repository) 
    : CrudManager<Stadium, StadiumDto, StadiumCreateDto, StadiumUpdateDto>(repository)
    , IStadiumService
{
    public async override Task<StadiumDto> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        var dto = _mapper.Map<StadiumDto>(entity);
        var capacity = 0;
        foreach (var s in entity.Sectors)
        {
            capacity += s.Seats.Count();
        }
        dto.Capacity = capacity;
        return dto;
    }

}
