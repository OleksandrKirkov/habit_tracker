using System.Security.Claims;
using Core.DTO.Habits;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller for managing user habits.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/habit")]
    public class HabitController : ControllerBase
    {
        private readonly IHabitService _habits;

        public HabitController(IHabitService habits)
        {
            _habits = habits;
        }

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        /// <summary>
        /// Get all habits of a specific user.
        /// </summary>
        /// <returns>A list of the user's habits.</returns>
        [HttpGet("my")]
        [ProducesResponseType(typeof(IEnumerable<Habit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUser()
        {
            var habits = await _habits.GetHabitsByUserAsync(GetUserId());
            return Ok(habits);
        }

        /// <summary>
        /// Get a specific habit by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <returns>The habit with related logs, if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Habit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            return Ok(habit);
        }

        /// <summary>
        /// Create a new habit for a user.
        /// </summary>
        /// <param name="request">The habit creation request data.</param>
        /// <returns>The newly created habit.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Habit), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateHabitRequest request)
        {
            var created = await _habits.CreateHabitAsync(request, GetUserId());
            return CreatedAtAction(nameof(GetById), new { id = GetUserId() }, created);
        }

        /// <summary>
        /// Archive a habit by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <returns>No content if archiving was successful.</returns>
        [HttpPost("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Archive(int id)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            await _habits.ArchiveHabitAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Delete a habit by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <returns>No content if deletion was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            var success = await _habits.DeleteHabitAsync(id);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update the frequency of a habit.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <param name="frequency">New frequency value (1-7).</param>
        /// <returns>No content if deletion was successful.</returns>
        [HttpPatch("{id}/frequency")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFrequency(int id, [FromBody] int frequency)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            var success = await _habits.UpdateFrequencyAsync(id, frequency);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update the reminder time of a habit.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <param name="time">New reminder time or null to remove it.</param>
        /// <returns>No content if update was successful.</returns>
        [HttpPatch("{id}/reminder-time")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReminderTime(int id, [FromBody] TimeSpan time)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            var success = await _habits.UpdateReminderTimeAsync(id, time);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update the reminder mode of a habit.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <param name="mode">New reminder mode (e.g., "daily", "weekly", or null).</param>
        /// <returns>No content if update was successful.</returns>
        [HttpPatch("{id}/reminder-mode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReminderMode(int id, [FromBody] string mode)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            var success = await _habits.UpdateReminderModeAsync(id, mode);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Enable or disable reminders for a habit.
        /// </summary>
        /// <param name="id">The unique identifier of the habit.</param>
        /// <param name="enabled">True to enable reminders, false to disable.</param>
        /// <returns>No content if update was successful.</returns>
        [HttpPatch("{id}/reminder-state")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SetReminderState(int id, [FromBody] bool enabled)
        {
            var habit = await _habits.GetByIdWithLogsAsync(id);
            if (habit == null || habit.UserId != GetUserId()) return NotFound();
            var success = await _habits.SetReminderStateAsync(id, enabled);
            return success ? NoContent() : NotFound();
        }

    }
}
