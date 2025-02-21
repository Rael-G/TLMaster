using System;

namespace TLMaster.Application.Interfaces;

public interface IAuthService
{
    Task<string> Login();
}
