using System.Linq.Expressions;
using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public List<Task<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(User entity)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }
}
