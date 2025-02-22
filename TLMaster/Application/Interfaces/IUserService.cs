using System;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Deletes an existing user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <param name="authenticatedUser">The authenticated user that requested this command.</param>
    /// <returns>The identity result</returns>
    Task<IdentityResult> Delete(UserDto user, User authenticatedUser);

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <param name="authenticatedUser">The authenticated user that requested this command.</param>
    /// <returns>The retrieved user, or null if not found.</returns>
    Task<UserDto?> GetById(Guid id, User authenticatedUser);

    Task<UserDto?> GetByUsername(string username, User authenticatedUser);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <param name="authenticatedUser">The authenticated user that requested this command.</param>
    /// <returns>A collection of user.</returns>
    Task<IEnumerable<UserDto>> GetAll(User authenticatedUser);
}
