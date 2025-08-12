using TicketSelling.Application.Dtos.GiftReward;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class GiftRewardManager(IRepository<GiftReward> repository) 
    : CrudManager<GiftReward, GiftRewardDto, GiftRewardCreateDto, GiftRewardUpdateDto>(repository)
    , IGiftRewardService
{
}