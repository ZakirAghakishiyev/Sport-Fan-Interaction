using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TicketSelling.Application.Interfaces;

public interface ICrudServiceWithoutUpdate<TEntity, TDto, TCreateDto>
{
    Task<TDto> GetByIdAsync(int id);
    Task<TDto> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<TDto> AddAsync(TCreateDto createDto);
    Task RemoveAsync(int id);
}
