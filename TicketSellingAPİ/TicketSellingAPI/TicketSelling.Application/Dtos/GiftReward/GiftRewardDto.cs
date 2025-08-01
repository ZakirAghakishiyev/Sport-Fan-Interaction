using TicketSelling.Application.Dtos.User;
using TicketSelling.Core.Entities;
namespace TicketSelling.Application.Dtos.GiftReward;

public class GiftRewardDto
{
    public int Id { get; set; }
    public RewardType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime GivenDate { get; set; }

    public UserDto? User { get; set; }  // Use a lightweight User DTO
}
