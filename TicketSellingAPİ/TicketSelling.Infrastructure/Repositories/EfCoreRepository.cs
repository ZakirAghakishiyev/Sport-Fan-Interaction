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

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public virtual T Get(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> queryable = _context.Set<T>();

        if (include != null)
            queryable = include(queryable);

        if (!asNoTracking)
            queryable = queryable.AsNoTracking();

        return queryable.FirstOrDefault(predicate);
    }

    public virtual List<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
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

        return queryable.ToList();
    }

    public virtual T GetById(int id)
    {
        T entity = _context.Set<T>().Find(id);

        return entity;
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
}
