using System;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
