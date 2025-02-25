using System;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);

    Task<RefreshToken> CreateRefreshToken(User user);

    Task<RefreshToken?> GetByToken(string token);

    Task RevokeRefreshToken(RefreshToken refreshToken);
}
