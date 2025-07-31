using System.Linq.Expressions;
using TicketSelling.Core.Entities;

namespace TicketSelling.Core.Interfaces;

public interface IUserRepository
{
    User GetById(int id);
    User Get(Expression<Func<User, bool>> predicate);
    List<User> GetAll();
    void Add(User entity);
    void Update(User entity);
    void Remove(User entity);
}