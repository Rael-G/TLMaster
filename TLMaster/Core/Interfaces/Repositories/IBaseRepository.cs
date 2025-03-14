using TLMaster.Core.Entities;

namespace TLMaster.Core.Interfaces.Repositories;


public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    void Create(T entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    void Delete(Guid id);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="track">Track this entity on Context</param>
    /// <returns>The retrieved entity, or null if not found.</returns>
    Task<T?> GetByIdFull(Guid id, bool track = false);

    Task<T?> GetById(Guid id, bool track = false);

    /// <summary>
    /// Retrieves all entities of type T.
    /// </summary>
    /// <returns>A collection of entities of type T.</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Commits any pending changes to the underlying data store.
    /// </summary>
    Task Commit();
}
