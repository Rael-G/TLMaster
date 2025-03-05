using System;
using AutoMapper;
using TLMaster.Application.Exceptions;
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

    public virtual async Task Create(TDto dto, Guid authenticatedUserId)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Create(entity);
        await Repository.Commit();
    }

    public virtual async Task Update(TDto dto, Guid authenticatedUserId)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Update(entity);
        await Repository.Commit();
    }

    public virtual async Task<bool> Delete(TDto dto, Guid authenticatedUserId)
    {
        var entity = Mapper.Map<TEntity>(dto);
        Repository.Delete(entity);
        await Repository.Commit();
        return true;
    }

    public virtual async Task<TDto?> GetByIdFull(Guid id, Guid authenticatedUserId)
    {
        var entity = await Repository.GetByIdFull(id);
        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto?> GetById(Guid id, Guid authenticatedUserId)
    {
        var entity = await Repository.GetById(id);
        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAll(Guid authenticatedUserId)
    {
        var entity = await Repository.GetAll();
        return Mapper.Map<IEnumerable<TDto>>(entity);
    }

    protected void ValidateIdentity(Guid entityId, Guid authenticatedUserId)
    {
        if (entityId != authenticatedUserId)
        {
            throw new ForbiddenAccessException("This user is not allowed to access this endpoint.");
        }
    }

}
