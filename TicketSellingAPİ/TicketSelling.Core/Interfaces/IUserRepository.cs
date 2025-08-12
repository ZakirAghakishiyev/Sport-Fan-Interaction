using System.Linq.Expressions;
using TicketSelling.Core.Entities;

namespace TicketSelling.Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(int id);
    Task<User> Get(Expression<Func<User, bool>> predicate);
    List<Task<User>> GetAll();
    void Add(User entity);
    void Update(User entity);
    void Remove(User entity);
}