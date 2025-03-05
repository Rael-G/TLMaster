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

    public virtual async Task<T?> GetByIdFull(Guid id)
    {
        IQueryable<T> query = Context.Set<T>().AsNoTracking();

        foreach (var navigation in Context.Model.FindEntityType(typeof(T))?.GetNavigations() ?? [])
        {
            query = query.Include(navigation.Name);
        }

        foreach (var skipNavigation in Context.Model.FindEntityType(typeof(T))?.GetSkipNavigations() ?? [])
        {
            query = query.Include(skipNavigation.Name);
        }

        return await query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public virtual async Task<T?> GetById(Guid id)
        => await Context.Set<T>()
        .Where(t => t.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();

    public virtual async Task<IEnumerable<T>> GetAll()
        => await Context.Set<T>()
        .AsNoTracking()
        .ToListAsync();

    public async Task Commit()
        => await Context.SaveChangesAsync();
}