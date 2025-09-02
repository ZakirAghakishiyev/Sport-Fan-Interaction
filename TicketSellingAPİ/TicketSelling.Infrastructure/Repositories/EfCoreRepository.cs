using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Infrastructure.Repositories;

public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public EfCoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            throw new NullReferenceException("Entity not found");
        return entity;
    }

    public async Task<T> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool asNoTracking = false)
    {
        IQueryable<T> queryable = _context.Set<T>();

        if (include != null)
            queryable = include(queryable);

        if (asNoTracking)
            queryable = queryable.AsNoTracking();

        var entity = await queryable.FirstOrDefaultAsync(predicate);
        if (entity == null)
            throw new NullReferenceException("Entity not found");

        return entity;
    }

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool asNoTracking = false)
    {
        IQueryable<T> queryable = _context.Set<T>();

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (include != null)
            queryable = include(queryable);

        if (orderBy != null)
            queryable = orderBy(queryable);

        if (asNoTracking)
            queryable = queryable.AsNoTracking();

        return await queryable.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }


    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
