using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly HabitService _habitService;

    public HabitsController(HabitService habitService)
    {
        _habitService = habitService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabitRequest request)
    {
        var habit = await _habitService.CreateHabitAsync(request.UserId, request.Name, request.Description, request.Frequency);
        return Ok(habit);
    }

    [HttpGet("{id}/checkins")]
    public async Task<IActionResult> GetWithCheckins(int id)
    {
        var habit = await _habitService.GetHabitWithCheckinsAsync(id);
        return habit is null ? NotFound() : Ok(habit);
    }
}

public class CreateHabitRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Frequency { get; set; } = "daily";
}
