using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Services;

public class UserService(UserManager<User> userManager, IMapper mapper)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<IdentityResult> Create(UserDto userDto, string password)
    {
        var user = _mapper.Map<User>(userDto);
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> Update(UserDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        if (user is null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        _mapper.Map(userDto, user);
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> Delete(UserDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        return user is not null ? await _userManager.DeleteAsync(user) : IdentityResult.Failed(new IdentityError { Description = "User not found" });
    }

        public async Task<UserDto?> GetById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user is not null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetByUsername(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user is not null ? _mapper.Map<UserDto>(user) : null;
    }

    public Task<IEnumerable<UserDto>> GetAll()
    {
        var users = _userManager.Users.ToList();
        return Task.FromResult(_mapper.Map<IEnumerable<UserDto>>(users));
    }
}

