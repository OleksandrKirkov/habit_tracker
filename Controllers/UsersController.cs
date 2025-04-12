using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] string username)
    {
        var user = await _userService.CreateUserAsync(username);
        return Ok(user);
    }

    [HttpGet("{id}/habits")]
    public async Task<IActionResult> GetUserHabits(int id)
    {
        var habits = await _userService.GetUserHabitsAsync(id);
        return Ok(habits);
    }
}
