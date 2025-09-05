using AutoMapper;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Dtos.User;
using TicketSelling.Core.Entities;
using TicketSelling.Web.Requests;

namespace TicketSelling.Web.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MerchandiseCreateDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseUpdateDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseUpdateRequest, MerchandiseUpdateDto>().ReverseMap();

        CreateMap<AppUser, UserDto>().ReverseMap();

        CreateMap<CardDetails, CardDetailsDto>().ReverseMap();

        CreateMap<CardDetails, CardDetailsCreateDto>().ReverseMap();
        CreateMap<CardDetailsUpdateRequest, CardDetailsDto>().ReverseMap();

    }
}
