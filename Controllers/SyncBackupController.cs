using System.Security.Claims;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for managing user sync backups.
/// </summary>
[Authorize]
[ApiController]
[Route("api/sync-backups")]
[Produces("application/json")]
public class SyncBackupController : ControllerBase
{
    private readonly ISyncBackupService _backups;

    public SyncBackupController(ISyncBackupService backups)
    {
        _backups = backups;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    /// Get all sync backups for the authenticated user.
    /// </summary>
    [HttpGet("my")]
    [ProducesResponseType(typeof(IEnumerable<SyncBackup>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUser()
    {
        var userId = GetUserId();
        var list = await _backups.GetByUserAsync(userId);
        return Ok(list);
    }

    /// <summary>
    /// Create a sync backup for the authenticated user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SyncBackup), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] SyncBackup model)
    {
        model.UserId = GetUserId();
        var created = await _backups.CreateAsync(model);
        return Ok(created);
    }
}
