using AutoMapper;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Dtos.Order;
using TicketSelling.Application.Dtos.OrderItem;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MerchandiseCreateDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseDto, Merchandise>().ReverseMap();   
        CreateMap<MerchandiseUpdateDto, Merchandise>().ReverseMap();

        CreateMap<CardDetails, CardDetailsDto>().ReverseMap();
        CreateMap<CardDetails, CardDetailsCreateDto>().ReverseMap();

        CreateMap<OrderCreateDto, Order>()
                        .ForMember(dest => dest.Payment, opt => opt.Ignore()) 
                        .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}
