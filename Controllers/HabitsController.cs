using Core.DTO.Habits;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller for managing user habits.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HabitController : ControllerBase
    {
        private readonly HabitService _habits;

        public HabitController(HabitService habits)
        {
            _habits = habits;
        }

        /// <summary>
        /// Get all habits of a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of the user's habits.</returns>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Habit>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var habits = await _habits.GetHabitsByUserAsync(userId);
            return Ok(habits);
        }

        /// <summary>
        /// Get a specific habit by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <returns>The habit with related logs, if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Habit>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null) return NotFound();
            return Ok(habit);
        }

        /// <summary>
        /// Create a new habit for a user.
        /// </summary>
        /// <param name="id">The habit creation request data.</param>
        /// <returns>The newly created habit.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Habit), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateHabitRequest request)
        {
            var created = await _habits.CreateHabitAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Archive a habit by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <returns>No content if archiving was successful.</returns>
        [HttpPost("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Archive(Guid id)
        {
            await _habits.ArchiveHabitAsync(id);
            return NoContent();
        }
    }
}
