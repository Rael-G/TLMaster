using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Application.Exceptions;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Services;

public class UserService(UserManager<User> userManager, RoleManager<User> roleManager, IMapper mapper)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<User> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;

    public async Task<IdentityResult> Delete(UserDto userDto, User authenticatedUser)
    {
        await ValidateIdentity(userDto.Id, authenticatedUser);
        
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        return user is not null ? await _userManager.DeleteAsync(user) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
    }

    public async Task<UserDto?> GetById(Guid id, User authenticatedUser)
    {
        await ValidateIdentity(id, authenticatedUser);

        var user = await _userManager.FindByIdAsync(id.ToString());
        return user is not null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetByUsername(string username, User authenticatedUser)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return null;
        }

        await ValidateIdentity(user.Id, authenticatedUser);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAll(User authenticatedUser)
    {
        // only admin
        await ValidateIdentity(Guid.NewGuid(), authenticatedUser);

        var users = _userManager.Users.ToList();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    private async Task ValidateIdentity(Guid entityId, User authenticatedUser)
    {
        if (!await IsAdmin(authenticatedUser) && entityId != authenticatedUser.Id)
        {
            throw new ForbiddenAccessException("This user is not allowed to access this endpoint.");
        }
    }

    private async Task<bool> IsAdmin(User authenticatedUser)
    {
        return await _roleManager.GetRoleNameAsync(authenticatedUser) == "admin";
    }
}

