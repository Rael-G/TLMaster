namespace TLMaster.Application.Interfaces;

public interface IBaseService<T> where T : IDto
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    Task Create(T entity, Guid authenticatedUserId);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    Task Update(T entity, Guid authenticatedUserId);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    /// <returns>True if the entity was deleted, False if not.</returns>
    Task<bool> Delete(T entity, Guid authenticatedUserId);

    /// <summary>
    /// Retrieves a entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    /// <returns>The retrieved entity, or null if not found.</returns>
    Task<T?> GetByIdFull(Guid id, Guid authenticatedUserId);

    Task<T?> GetById(Guid id, Guid authenticatedUserId);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    /// <returns>A collection of entity.</returns>
    Task<IEnumerable<T>> GetAll(Guid authenticatedUserId);

}
