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
    [AllowAnonymous]
    public class AuthController(SignInManager<User> signInManager, IAuthService authService)
        : ControllerBase
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Initiates the login process using an external authentication provider (Discord).
        /// Redirects the user to the authentication provider for login.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect the user after successful authentication. Defaults to "/".</param>
        /// <returns>An authentication challenge that redirects the user to the external login provider.</returns>
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl }, protocol: Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(DiscordAuthenticationDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Handles the callback from the external authentication provider.
        /// If authentication is successful, generates and returns an access token and a refresh token.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect the user after login. Optional.</param>
        /// <param name="remoteError">An error message returned by the external authentication provider, if any. Optional.</param>
        /// <returns>
        /// A JSON response containing the access token and refresh token if authentication is successful.
        /// Returns a 400 Bad Request if an external authentication error occurs.
        /// Returns a 401 Unauthorized if authentication fails.
        /// </returns>
        [HttpGet("external-login-callback")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                return BadRequest(new { error = $"External login error: {remoteError}" });
            }

            string token;
            string refreshToken;
            try
            {
                (token, refreshToken) = await _authService.Login();
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(new {AccessToken = token, RefreshToken = refreshToken});
        }

        /// <summary>
        /// Regenerates an access token using a refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token provided by a previous authentication session.</param>
        /// <returns>
        /// A JSON response containing a new access token and refresh token if successful.
        /// Returns a 401 Unauthorized if the refresh token is invalid or expired.
        /// </returns>
        [HttpPost("regen-token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegenToken([FromBody] string refreshToken)
        {
            string token;
            try
            {
                (token, refreshToken) = await _authService.RegenToken(refreshToken);
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(new {AccessToken = token, RefreshToken = refreshToken});
        }
        
    }
}
