using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for managing achievement.
/// </summary>
[ApiController]
[Route("api/achievements")]
[Produces("application/json")]
public class AchievementController : ControllerBase
{
    private readonly IAchievementService _achievements;

    public AchievementController(IAchievementService achievement)
    {
        _achievements = achievement;
    }

    /// <summary>
    /// Get all available achievements.
    /// </summary>
    /// <returns>List of achievements.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Achievement>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var list = await _achievements.GetAllAsync();
        return Ok(list);
    }

    /// <summary>
    /// Get achievement by its unique code.
    /// </summary>
    /// <param name="code">Unique code of the achievement.</param>
    /// <returns>The achievement if found.</returns>
    [HttpGet("{code}")]
    [ProducesResponseType(typeof(Achievement), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCode(string code)
    {
        var achievement = await _achievements.GetByCodeAsync(code);
        if (achievement == null) return NotFound();
        return Ok(achievement);
    }

    /// <summary>
    /// Create a new achievement.
    /// </summary>
    /// <param name="model">Achievement data.</param>
    /// <returns>The created achievement with its code.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Achievement), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] Achievement model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid achievement data");

        var created = await _achievements.CreateAsync(model);
        return CreatedAtAction(nameof(GetByCode), new { code = created.Code }, created);
    }
}
