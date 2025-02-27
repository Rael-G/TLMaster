using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application.Dtos;
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
        public IActionResult Login(string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(GetToken), "Auth", null, protocol: Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(DiscordAuthenticationDefaults.AuthenticationScheme, returnUrl);
            return Challenge(properties, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Generates and returns an authentication token.
        /// </summary>
        /// <returns>
        /// A JSON response containing the authentication token if successful.
        /// Returns a 401 Unauthorized if authentication fails.
        /// </returns>
        [HttpGet("get-token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToken()
        {
            Console.WriteLine(Request.Host);

            TokenDto token;
            try
            {
                token = await _authService.GetToken();
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(token);
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
            TokenDto token;
            try
            {
                token = await _authService.RegenToken(refreshToken);
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(token);
        }
        
    }
}
