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
    T GetById(int id);
    T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    List<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}
