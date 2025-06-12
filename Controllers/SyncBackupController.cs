using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncBackupController : ControllerBase
    {
        private readonly ISyncBackupService _backups;

        public SyncBackupController(ISyncBackupService backups)
        {
            _backups = backups;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var list = await _backups.GetByUserAsync(userId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SyncBackup model)
        {
            var created = await _backups.CreateAsync(model);
            return Ok(created);
        }
    }
}
