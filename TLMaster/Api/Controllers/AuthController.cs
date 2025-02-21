using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SignInManager<User> signInManager, IAuthService authService)
        : ControllerBase
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IAuthService _authService = authService;

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl }, protocol: Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(DiscordAuthenticationDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("external-login-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                return BadRequest(new { error = $"External login error: {remoteError}" });
            }

            string token;
            try
            {
                token = await _authService.Login();
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(token);
        }
        
    }
}
