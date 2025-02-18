using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public abstract class BaseRepository<T>(ApplicationDbContext context)
    : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context = context;

    public virtual void Create(T entity)
        => Context.Add(entity);

    public virtual void Update(T entity)
        => Context.Update(entity);

    public virtual void Delete(T entity)
        => Context.Remove(entity);

    public virtual async Task<T?> GetById(Guid id)
        => await Context.Set<T>()
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id);

    public virtual async Task<IEnumerable<T>> GetAll()
        => await Context.Set<T>()
        .AsNoTracking()
        .ToListAsync();

    public async Task Commit()
        => await Context.SaveChangesAsync();
}