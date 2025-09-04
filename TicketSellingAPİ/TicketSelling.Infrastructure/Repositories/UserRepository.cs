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

    public void Add(AppUser entity)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> Get(Expression<Func<AppUser, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public List<Task<AppUser>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(AppUser entity)
    {
        throw new NotImplementedException();
    }

    public void Update(AppUser entity)
    {
        throw new NotImplementedException();
    }
}
