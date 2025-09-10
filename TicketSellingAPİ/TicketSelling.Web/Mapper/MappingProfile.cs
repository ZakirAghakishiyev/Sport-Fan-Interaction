using AutoMapper;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Dtos.Sector;
using TicketSelling.Application.Dtos.Stadium;
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

        CreateMap<StadiumCreateDto, Stadium>().ReverseMap();
        CreateMap<StadiumUpdateDto, Stadium>().ReverseMap();
        CreateMap<Stadium, StadiumDto>().ReverseMap();

        CreateMap<Sector, SectorDto>()
            .ForMember(dest => dest.StadiumName, opt => opt.MapFrom(src => src.Stadium!.Name));
        CreateMap<SectorCreateDto, Sector>();
        CreateMap<SectorUpdateDto, Sector>();

        CreateMap<Seat, SeatDto>()
                   .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector));
        CreateMap<SeatCreateDto, Seat>();
        CreateMap<SeatUpdateDto, Seat>();
        CreateMap<Seat, SeatUpdateDto>();

        CreateMap<MatchCreateDto, Match>().ReverseMap();
        CreateMap<MatchUpdateDto, Match>().ReverseMap();
        CreateMap<MatchDto, Match>().ReverseMap();
    }
}
