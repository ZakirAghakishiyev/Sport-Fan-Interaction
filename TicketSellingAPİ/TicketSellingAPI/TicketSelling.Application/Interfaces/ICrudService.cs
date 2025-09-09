using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Interfaces;

public interface ICrudService<TEntity, TDto, TCreateDto, TUpdateDto>
{
    Task<TDto> GetByIdAsync(int id);
    Task<TDto> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<TDto> AddAsync(TCreateDto createDto);
    Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto);
    Task RemoveAsync(int id);
}
