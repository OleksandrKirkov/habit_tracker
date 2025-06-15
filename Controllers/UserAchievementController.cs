using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for assigning and retrieving user achievements.
/// </summary>
[Authorize]
[ApiController]
[Route("api/user-achievements")]
[Produces("application/json")]
public class UserAchievementController : ControllerBase
{
    private readonly IUserAchievementService _service;

    public UserAchievementController(IUserAchievementService service)
    {
        _service = service;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    /// Get all achievements assigned to the authenticated user.
    /// </summary>
    /// <returns>List of user achievements.</returns>
    [HttpGet("my")]
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUser()
    {
        int userId = GetUserId();
        var achievement = await _service.GetByUserAsync(userId);
        return Ok(achievement);
    }

    /// <summary>
    /// Assign an achievement to the authenticated user.
    /// </summary>
    /// <param name="request">Achievement assignment data</param>
    /// <returns>Assigned achievement if successful.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Assign([FromBody] AssignRequest request)
    {
        int userId = GetUserId();
        var result = await _service.AssignAsync(userId, request.AchievementId);
        if (result == null)
            return Conflict("Achievement already assigned.");
        return Ok(result);
    }

    public class AssignRequest
    {
        public int AchievementId { get; set; }
    }
}
