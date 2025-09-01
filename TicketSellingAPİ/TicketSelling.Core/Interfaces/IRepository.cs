using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Core.Entities;

namespace TicketSelling.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);

    Task<T> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool asNoTracking = false);

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool asNoTracking = false);

    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}
