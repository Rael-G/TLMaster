using System;
using TLMaster.Core.Entities;

namespace TLMaster.Core.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves an user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The retrieved user, or null if not found.</returns>
    Task<User?> Get(Guid id);

    /// <summary>
    /// Retrieves all entities of type User.
    /// </summary>
    /// <returns>A collection of entities of type User.</returns>
    Task<IEnumerable<User>> GetAll();

    /// <summary>
    /// Commits any pending changes to the underlying data store.
    /// </summary>
    Task Commit();
}
