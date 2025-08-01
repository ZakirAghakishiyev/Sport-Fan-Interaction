using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.GiftReward
{
    public class GiftRewardUpdateDto
    {
        public RewardType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime GivenDate { get; set; }
    }

}
