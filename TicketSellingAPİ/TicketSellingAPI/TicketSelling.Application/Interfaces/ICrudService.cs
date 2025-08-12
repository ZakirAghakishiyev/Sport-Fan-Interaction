using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TicketSelling.Application.Interfaces;

public interface ICrudService<TEntity, TDto, TCreateDto, TUpdateDto>
{
    Task<TDto> GetById(int id);
    Task<TDto> Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    Task<List<TDto>> GetAll(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    void AddAsync(TCreateDto createDto);
    void Update(TUpdateDto updateDto);
    void Remove(int id);
}
