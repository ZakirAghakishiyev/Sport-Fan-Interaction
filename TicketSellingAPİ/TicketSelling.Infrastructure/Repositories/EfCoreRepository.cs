using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Core.Interfaces;
using TicketSelling.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketSelling.Infrastructure.Repositories;

public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public EfCoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async virtual Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async virtual Task<T> GetAsync(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> queryable = _context.Set<T>();

        if (include != null)
            queryable = include(queryable);

        if (!asNoTracking)
            queryable = queryable.AsNoTracking();
        var res = await queryable.FirstOrDefaultAsync(predicate);
        if (res == null)
            throw new NullReferenceException(nameof(res));
        return res;
    }

    public async virtual Task<List<T>> GetAll(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> queryable = _context.Set<T>();

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (include != null)
            queryable = include(queryable);

        if (orderBy != null)
            queryable = orderBy(queryable);

        if (!asNoTracking)
            queryable = queryable.AsNoTracking();

        return await queryable.ToListAsync();
    }

    public async virtual Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if(entity == null)
            throw new NullReferenceException($"{nameof(entity)} is null");
        return entity;
    }

    public async virtual void RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async virtual void UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
