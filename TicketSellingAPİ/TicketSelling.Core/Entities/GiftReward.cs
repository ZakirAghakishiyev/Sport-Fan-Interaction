namespace TicketSelling.Core.Entities
{
    public class GiftReward: BaseEntity
    {
        public Guid UserId { get; set; }
        public RewardType Type { get; set; }
        public string Description { get; set; }
        public DateTime GivenDate { get; set; }

        public User User { get; set; }
    }


}
