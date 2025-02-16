using System;
using AutoMapper;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public abstract class BaseService<TDto, TEntity>
    (IBaseRepository<TEntity> repository, IMapper mapper)
        : IBaseService<TDto>
            where TDto : IDto 
            where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> Repository = repository;
    protected readonly IMapper Mapper = mapper;

    public virtual async Task Create(TDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Create(entity);
        await Repository.Commit();
    }

    public virtual async Task Update(TDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Update(entity);
        await Repository.Commit();
    }

    public virtual async Task<bool> Delete(TDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Delete(entity);
        await Repository.Commit();
        return true;
    }

    public virtual async Task<TDto?> Get(Guid id)
    {
        var entity = await Repository.Get(id);
        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAll()
    {
        var entity = await Repository.GetAll();
        return Mapper.Map<IEnumerable<TDto>>(entity);
    }
}
