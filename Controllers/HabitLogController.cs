using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for logging habit completions.
/// </summary>
[Authorize]
[ApiController]
[Route("api/habit-logs")]
[Produces("application/json")]
public class HabitLogController : ControllerBase
{
    private readonly IHabitLogService _logs;

    public HabitLogController(IHabitLogService logs)
    {
        _logs = logs;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    /// Get all logs for a specific habit belonging to the current user.
    /// </summary>
    /// <param name="habitId">The ID of the habit.</param>
    /// <returns>List of habit logs.</returns>
    [HttpGet("habit/{habitId}")]
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByHabit(int habitId)
    {
        var userId = GetUserId();
        var logs = await _logs.GetLogsByHabitAsync(habitId, userId);
        return Ok(logs);
    }

    /// <summary>
    /// Log habit completion for a specific date.
    /// </summary>
    /// <param name="habitId">The ID of the habit.</param>
    /// <param name="request">Log date and optional value.</param>
    /// <returns>Create log entry.</returns>
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status409Conflict)]
    [HttpPost("{habitId}/log")]
    public async Task<IActionResult> Log(int habitId, [FromBody] LogRequest request)
    {
        var userId = GetUserId();
        var alreadyLogged = await _logs.AlreadyLoggedAsync(habitId, userId, request.Date);
        if (alreadyLogged)
            return Conflict("Already logged for this date.");

        var log = await _logs.LogAsync(habitId, userId, request.Date, request.Value);
        return Ok(log);
    }
}

public class LogRequest
{
    public DateTime Date { get; set; }
    public int? Value { get; set; }
}
