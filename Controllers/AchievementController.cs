using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService _achievements;

        public AchievementController(IAchievementService achievement)
        {
            _achievements = achievement;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _achievements.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var achievement = await _achievements.GetByCodeAsync(code);
            if (achievement == null) return NotFound();
            return Ok(achievement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Achievement model)
        {
            var created = await _achievements.CreateAsync(model);
            return CreatedAtAction(nameof(GetByCode), new { code = created.Code }, created);
        }
    }
}
