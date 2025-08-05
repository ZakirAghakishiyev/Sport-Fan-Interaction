using TicketSelling.Application.Dtos.UserSavedCards;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IUserSavedCardService : ICrudServiceWithoutUpdate<UserSavedCard, UserSavedCardDto, UserSavedCardCreateDto> { }