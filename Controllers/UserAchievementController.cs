using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAchievementController : ControllerBase
    {
        private readonly IUserAchievementService _service;

        public UserAchievementController(IUserAchievementService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var achievement = await _service.GetByUserAsync(userId);
            return Ok(achievement);
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AssignRequest request)
        {
            var result = await _service.AssignAsync(request.UserId, request.AchievementId);
            if (result == null)
                return Conflict("Achievement already assigned.");
            return Ok(result);
        }

        public class AssignRequest
        {
            public int UserId { get; set; }
            public int AchievementId { get; set; }
        }
    }
}
