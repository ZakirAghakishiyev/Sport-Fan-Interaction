using TicketSelling.Application.Dtos.GiftReward;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface IGiftReward : ICrudService<GiftReward, GiftRewardDto, GiftRewardCreateDto, GiftRewardUpdateDto> { }