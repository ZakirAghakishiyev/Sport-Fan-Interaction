using TicketSelling.Application.Dtos.UserSavedCards;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class UserSavedCardManager(IRepository<UserSavedCard> repository) 
    : CrudManagerWithoutUpdate<UserSavedCard, UserSavedCardDto, UserSavedCardCreateDto>(repository)
    , IUserSavedCardService
{
}
