using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Core.Entities;

namespace TicketSelling.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    void AddAsync(T entity);
    void UpdateAsync(T entity);
    void RemoveAsync(T entity);
}
