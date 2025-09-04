namespace TicketSelling.Core.Entities
{
    public class GiftReward: BaseEntity
    {
        public int UserId { get; set; }
        public RewardType Type { get; set; }
        public string? Description { get; set; }
        public DateTime GivenDate { get; set; }

        public AppUser? User { get; set; }
    }
}
