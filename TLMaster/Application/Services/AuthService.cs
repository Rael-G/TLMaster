using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Services;

public class AuthService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
    : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

        public async Task<string> Login()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync() 
                ?? throw new UnauthorizedAccessException("Error retrieving info from external login.");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("Email not provided by external login.");
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new User { UserName = email, Email = email };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new UnauthorizedAccessException("Failed to create User.");
                    }
                }

                await _userManager.AddLoginAsync(user, info);
            }

            var token = _tokenService.GenerateToken(user);

            return token;
        }
}
