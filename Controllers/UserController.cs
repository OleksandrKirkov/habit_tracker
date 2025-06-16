using System.Security.Claims;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for user operations (creation, lookup, identity).
/// </summary>
[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly UserService _users;

    public UserController(UserService users)
    {
        _users = users;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    /// Get information about the authenticated user.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var id = GetUserId();
        var user = await _users.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Check if an email is already registered.
    /// </summary>
    /// <param name="email">Email to check.</param>
    [HttpGet("exists")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckEmail([FromQuery] string email)
    {
        var exists = await _users.EmailExistsAsync(email);
        return Ok(new { exists });
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="user">User registration info.</param>
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        var exists = await _users.EmailExistsAsync(user.Email);
        if (exists)
            return Conflict("Email already in use.");

        var created = await _users.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetCurrentUser), new { }, created);
    }
}
