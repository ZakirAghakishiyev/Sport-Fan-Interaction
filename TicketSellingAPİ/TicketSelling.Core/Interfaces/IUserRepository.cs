using System.Linq.Expressions;
using TicketSelling.Core.Entities;

namespace TicketSelling.Core.Interfaces;

public interface IUserRepository
{
    Task<AppUser> GetById(int id);
    Task<AppUser> Get(Expression<Func<AppUser, bool>> predicate);
    List<Task<AppUser>> GetAll();
    void Add(AppUser entity);
    void Update(AppUser entity);
    void Remove(AppUser entity);
}