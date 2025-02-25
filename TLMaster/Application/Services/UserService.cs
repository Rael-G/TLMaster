using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Application.Enums;
using TLMaster.Application.Exceptions;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Services;

public class UserService(UserManager<User> userManager, IMapper mapper)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<IdentityResult> Delete(UserDto userDto, Guid authenticatedUserId)
    {
        await ValidateIdentity(userDto.Id, authenticatedUserId);
        
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        return user is not null ? await _userManager.DeleteAsync(user) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
    }

    public async Task<UserDto?> GetById(Guid id, Guid authenticatedUserId)
    {
        await ValidateIdentity(id, authenticatedUserId);

        var user = await _userManager.FindByIdAsync(id.ToString());
        return user is not null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetByUsername(string username, Guid authenticatedUserId)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return null;
        }

        await ValidateIdentity(user.Id, authenticatedUserId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<string>?> GetRoles(Guid userId, Guid authenticatedUserId)
    {
        await ValidateIdentity(userId, authenticatedUserId);
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null)
        {
            return null;
        }

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> UpdateRoles(Guid userId, string[] roles, Guid authenticatedUserId)
    {
        await ValidateIdentity(Guid.NewGuid(), authenticatedUserId);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false;
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            return false;
        }

        var addResult = await _userManager.AddToRolesAsync(user, roles);
        return addResult.Succeeded; 
    }

    public async Task<IEnumerable<UserDto>> GetAll(Guid authenticatedUserId)
    {
        // only admin
        await ValidateIdentity(Guid.NewGuid(), authenticatedUserId);

        var users = _userManager.Users.ToList();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    private async Task ValidateIdentity(Guid entityId, Guid authenticatedUserId)
    {
        if (!await IsAdmin(authenticatedUserId) && entityId != authenticatedUserId)
        {
            throw new ForbiddenAccessException("This user is not allowed to access this endpoint.");
        }
    }

    private async Task<bool> IsAdmin(Guid authenticatedUser)
    {
        var user = await _userManager.FindByIdAsync(authenticatedUser.ToString());
        
        if (user == null) return false;
        return await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);
    }
}

