using System;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Interfaces;

public interface IAuthService
{
    Task<(string, string)> Login();

    Task<(string, string)> RegenToken(string refreshToken);
}
