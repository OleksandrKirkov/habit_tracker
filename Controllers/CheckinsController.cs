using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckinsController : ControllerBase
{
    private readonly HabitCheckinService _checkinService;

    public CheckinsController(HabitCheckinService checkinService)
    {
        _checkinService = checkinService;
    }

    [HttpPost("{habitId}")]
    public async Task<IActionResult> CheckIn(int habitId)
    {
        var checkin = await _checkinService.CheckinAsync(habitId);
        return Ok(checkin);
    }

    [HttpGet("{habitId}")]
    public async Task<IActionResult> GetCheckins(int habitId)
    {
        var checkins = await _checkinService.GetCheckinsAsync(habitId);
        return Ok(checkins);
    }
}
