using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TicketSelling.Application.Interfaces;
using TicketSelling.Application.Mapper;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;

namespace TicketSelling.Application.Services;

public class CrudManager<TEntity, TDto, TCreateDto, TUpdateDto> : ICrudService<TEntity, TDto, TCreateDto, TUpdateDto> where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public CrudManager(IRepository<TEntity> repository)
    {
        _repository = repository;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    public async virtual Task<TDto> AddAsync(TCreateDto createDto)
    {
        TEntity entity = _mapper.Map<TEntity>(createDto);

        var res=await _repository.AddAsync(entity);
        return _mapper.Map<TDto>(res);
    }

    public async virtual Task<TDto> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var entity = await _repository.GetAsync(predicate, include);
        var dto = _mapper.Map<TDto>(entity);

        return dto;
    }

    public async virtual Task<List<TDto> >GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var entities = await _repository.GetAllAsync(predicate, include, orderBy);
        var dtos = _mapper.Map<List<TDto>>(entities);

        return dtos;
    }

    public async virtual Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        var dto = _mapper.Map<TDto>(entity);

        return dto;
    }

    public async virtual Task RemoveAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        await _repository.RemoveAsync(entity);
    }

    public async virtual Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return default;

        _mapper.Map(updateDto, entity); 
        var res = await _repository.UpdateAsync(entity);
        return _mapper.Map<TDto>(res);
    }

}
