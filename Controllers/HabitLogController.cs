using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HabitLogController : ControllerBase
    {
        private readonly HabitLogService _logs;

        public HabitLogController(HabitLogService logs)
        {
            _logs = logs;
        }

        [HttpGet("habit/{habitId}")]
        public async Task<IActionResult> GetByHabit(int habitId)
        {
            var logs = await _logs.GetLogsByHabitAsync(habitId);
            return Ok(logs);
        }

        [HttpPost("{habitId}/log")]
        public async Task<IActionResult> Log(int habitId, [FromBody] LogRequest request)
        {
            var alreadyLogged = await _logs.AlreadyLoggedAsync(habitId, request.Date);
            if (alreadyLogged)
                return Conflict("Already logged for this date.");

            var log = await _logs.LogAsync(habitId, request.Date, request.Value);
            return Ok(log);
        }
    }

    public class LogRequest
    {
        public DateTime Date { get; set; }
        public int? Value { get; set; }
    }
}
