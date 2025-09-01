using AutoMapper;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MerchandiseCreateDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseUpdateDto, Merchandise>().ReverseMap();   
    }
}
