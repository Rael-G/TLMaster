namespace TLMaster.Application.Interfaces;

public interface IBaseService<T> where T : IDto
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    Task Create(T entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    Task Update(T entity);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>True if the entity was deleted, False if not.</returns>
    Task<bool> Delete(T entity);

    /// <summary>
    /// Retrieves a entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>The retrieved entity, or null if not found.</returns>
    Task<T?> GetById(Guid id);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <returns>A collection of entity.</returns>
    Task<IEnumerable<T>> GetAll();

}
