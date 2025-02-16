using System;

namespace TLMaster.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    Task Create(UserDto user);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    Task Update(UserDto user);

    /// <summary>
    /// Deletes an existing user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <returns>True if the user was deleted, False if not.</returns>
    Task<bool> Delete(UserDto user);

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The retrieved user, or null if not found.</returns>
    Task<UserDto?> Get(Guid id);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <returns>A collection of user.</returns>
    Task<IEnumerable<UserDto>> GetAll();
}
