namespace TicketSelling.Core.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
    }
    public abstract class BaseEntity:IBaseEntity
    {
        public int Id { get; set; }

    }
}
