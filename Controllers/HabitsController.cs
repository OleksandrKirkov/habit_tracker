using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitController : ControllerBase
    {
        private readonly HabitService _habits;

        public HabitController(HabitService habits)
        {
            _habits = habits;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userid)
        {
            var habits = await _habits.GetHabitsByUserAsync(userid);
            return Ok(habits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null) return NotFound();
            return Ok(habit);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Habit habit)
        {
            var created = await _habits.CreateHabitAsync(habit);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> Archive(Guid id)
        {
            await _habits.ArchiveHabitAsync(id);
            return NoContent();
        }
    }
}
