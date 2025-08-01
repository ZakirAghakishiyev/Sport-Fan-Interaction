using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.GiftReward
{
    public class GiftRewardCreateDto
    {
        public int UserId { get; set; }
        public RewardType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime GivenDate { get; set; }
    }

}
