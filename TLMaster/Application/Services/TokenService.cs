using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
    : ITokenService
{
    /// <summary>
    /// Specifies the default expiration time for access tokens in minutes.
    /// </summary>
    public const int MinutesToExpiry = 30;

    /// <summary>
    /// Specifies the default expiration time for refresh tokens in days.
    /// </summary>
    public const int DaysToExpiry = 7;

    private readonly IConfiguration _configuration = configuration;

    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    private readonly UserManager<User> _userManager = userManager;

    private SecurityKey SecretKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]??
        throw new Exception("JWT Secret not defined in configuration.")));

    /// <summary>
    /// Generates an authentication token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The generated authentication token.</returns>
    public async Task<string> GenerateAccessToken(User user)
        => GenerateAccessToken(await GenerateClaimsIdentity(user));

    /// <summary>
    /// Generates a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is generated.</param>
    /// <returns>The generated refresh token.</returns>
    public async Task<RefreshToken> CreateRefreshToken(User user)
    {
        var refreshToken =  new RefreshToken
        (
            user,
            DateTime.UtcNow.AddDays(DaysToExpiry)
        );

        _refreshTokenRepository.Create(refreshToken);
        await _refreshTokenRepository.Commit();

        return refreshToken;
    }

    public async Task RevokeRefreshToken(RefreshToken refreshToken)
    {
        refreshToken.Revoke();

        // avoid dbcontext tracking issues 
        refreshToken.User = null!;
        
        _refreshTokenRepository.Update(refreshToken);
        await _refreshTokenRepository.Commit();
    }

    public virtual async Task<RefreshToken?> GetByToken(string id)
        => await _refreshTokenRepository.GetById(Guid.Parse(id));

    // Generates a JWT token based on the provided claims identity.
    private string GenerateAccessToken(ClaimsIdentity claimsIdentity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                SecretKey,
                SecurityAlgorithms.HmacSha256Signature),

            Expires = DateTime.UtcNow.AddMinutes(MinutesToExpiry),

            Subject = claimsIdentity,

            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // Generates the claims for the JWT token
    private async Task<ClaimsIdentity> GenerateClaimsIdentity(User user)
    {
        var claims = new List<Claim>(
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ]);

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return new ClaimsIdentity(claims);
    }
}
