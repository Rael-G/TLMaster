using System;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;

namespace TLMaster.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="password">The user's password</param>
    /// <returns>The identity result</returns>
    Task<IdentityResult> Create(UserDto user, string password);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <returns>The identity result</returns>
    Task<IdentityResult> Update(UserDto user);

    /// <summary>
    /// Deletes an existing user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <returns>The identity result</returns>
    Task<IdentityResult> Delete(UserDto user);

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The retrieved user, or null if not found.</returns>
    Task<UserDto?> GetById(Guid id);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <returns>A collection of user.</returns>
    Task<IEnumerable<UserDto>> GetAll();
}
