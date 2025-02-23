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
    /// <param name="authenticatedUserId">The authenticated user id that requested this command.</param>
    /// <returns>The identity result</returns>
    Task<IdentityResult> Delete(UserDto user, Guid authenticatedUserId);

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <param name="authenticatedUser">The authenticated user id that requested this command.</param>
    /// <returns>The retrieved user, or null if not found.</returns>
    Task<UserDto?> GetById(Guid id, Guid authenticatedUser);

    Task<UserDto?> GetByUsername(string username, Guid authenticatedUser);

    /// <summary>
    /// Retrieves all entitys.
    /// </summary>
    /// <param name="authenticatedUser">The authenticated user id that requested this command.</param>
    /// <returns>A collection of user.</returns>
    Task<IEnumerable<UserDto>> GetAll(Guid authenticatedUser);
}
