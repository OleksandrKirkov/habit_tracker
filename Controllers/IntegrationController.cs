using System.Security.Claims;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

/// <summary>
/// Controller for managing user integrations.
/// </summary>
[Authorize]
[ApiController]
[Route("api/integrations")]
[Produces("application/json")]
public class IntegrationController : ControllerBase
{
    private readonly IIntegrationService _integrations;

    public IntegrationController(IIntegrationService integrations)
    {
        _integrations = integrations;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    /// Get all integrations for the authenticated user.
    /// </summary>
    /// <returns>List of integrations.</returns>
    [HttpGet("my")]
    [ProducesResponseType(typeof(IEnumerable<Integration>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUser()
    {
        var userId = GetUserId();
        var list = await _integrations.GetByUserAsync(userId);
        return Ok(list);
    }

    /// <summary>
    /// Get integration by provider for the authenticated user.
    /// </summary>
    /// <param name="provider">Provider name (e.g "google").</param>
    /// <returns>Integration if exists.</returns>
    [HttpGet("my/{provider}")]
    [ProducesResponseType(typeof(Integration), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByProvider(string provider)
    {
        var userId = GetUserId();
        var integration = await _integrations.GetByProviderAsync(userId, provider);
        if (integration == null) return NotFound();
        return Ok(integration);
    }

    /// <summary>
    /// Create integration for the authenticated user.
    /// </summary>
    /// <param name="model">Integration details (provider, token, etc).</param>
    /// <returns>The created integration.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Integration), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] Integration model)
    {
        var userId = GetUserId();
        model.UserId = userId;
        var created = await _integrations.CreateAsync(model);
        return Ok(created);
    }
}
