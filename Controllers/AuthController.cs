using Core.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for auth.
/// </summary>
[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="request">User registration details</param>
    /// <returns>Access token in body, refresh token in cookie</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var token = await _authService.RegisterAsync(request);
        return Ok(token);
    }

    /// <summary>
    /// Log in with existing credentials.
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>Access token in body, refresh token in cookie</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);
        return Ok(token);
    }

    /// <summary>
    /// Refresh access token
    /// </summary>
    /// <returns>New access token</returns>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Refresh()
    {
        var token = await _authService.RefreshAsync();
        return Ok(token);
    }

    /// <summary>
    /// Logout current user
    /// </summary>
    [HttpPost("logout")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return Ok();
    }

    /// <summary>
    /// Get info about current user
    /// </summary>
    /// <returns>User details</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(MeResponse), 200)]
    [ProducesResponseType(typeof(string), 401)]
    public async Task<IActionResult> Me()
    {
        var user = await _authService.GetMeAsync(User);
        return Ok(user);
    }
}
