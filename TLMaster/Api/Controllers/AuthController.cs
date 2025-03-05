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
        /// Generates and sets authentication tokens as HTTP-only cookies.
        /// </summary>
        /// <returns>
        /// Returns a 204 No Content response if the tokens are successfully generated and set as cookies.
        /// Returns a 401 Unauthorized response if authentication fails.
        /// </returns>
        [HttpPost("generate-token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateToken()
        {
            TokenDto token;
            try
            {
                token = await _authService.GetToken();
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }

            Response.Cookies.Append("AccessToken", token.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });
            Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return NoContent();
        }

        /// <summary>
        /// Retrieves the stored access and refresh tokens from the client's cookies.
        /// </summary>
        /// <returns>
        /// Returns tokens if found.
        /// Returns a 401 Unauthorized response if the tokens are missing.
        /// </returns>
        [HttpGet("get-token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetToken()
        {
            if (!Request.Cookies.ContainsKey("AccessToken")
                || !Request.Cookies.ContainsKey("RefreshToken"))
            {
                return Unauthorized("Token not found.");
            }

            string accessToken = Request.Cookies["AccessToken"]!;
            string refreshToken = Request.Cookies["RefreshToken"]!;
            
            var tokenDto = new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };

            return Ok(tokenDto);
        }

        /// <summary>
        /// Regenerates and sets new authentication tokens using a refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token provided by a previous authentication session.</param>
        /// <returns>
        /// Returns a 204 No Content response if new tokens are successfully generated and set as cookies.
        /// Returns a 401 Unauthorized response if the refresh token is invalid or expired.
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

            Response.Cookies.Append("AccessToken", token.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });
            Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return NoContent();
        }

        /// <summary>
        /// Logs out the user by deleting the authentication cookies.
        /// </summary>
        /// <returns>
        /// Returns 204 No Content indicating the result of the operation.
        /// </returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");

            return NoContent();
        }
        
    }
}
