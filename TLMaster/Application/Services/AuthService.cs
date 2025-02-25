using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Services;

public class AuthService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
    : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<TokenDto> Login()
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

        var token = await _tokenService.GenerateAccessToken(user);
        var refreshToken = (await _tokenService.CreateRefreshToken(user)).Token;

        return new () { AccessToken = token, RefreshToken = refreshToken };
    }

    public async Task<TokenDto> RegenToken(string refreshToken)
    {
        var storedRefreshToken = await _tokenService.GetByToken(refreshToken);

        if (storedRefreshToken == null || storedRefreshToken.IsRevoked)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }

        if (storedRefreshToken.ExpirationDate < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired");
        }
            
        var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId.ToString()) 
            ?? throw new UnauthorizedAccessException("User is not recognized.");
        
        var newAccessToken = await _tokenService.GenerateAccessToken(user);
        var newRefreshToken = await _tokenService.CreateRefreshToken(user);

        await _tokenService.RevokeRefreshToken(storedRefreshToken);

        return new () { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token };

    }
}
