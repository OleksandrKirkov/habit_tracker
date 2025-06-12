using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _users;

        public UserController(UserService users)
        {
            _users = users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("exists")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            var exists = await _users.EmailExistsAsync(email);
            return Ok(new { exists });
        }

        [HttpPost]
        public async Task<IActionResult> Result([FromBody] User user)
        {
            var exists = await _users.EmailExistsAsync(user.Email);
            if (exists)
                return Conflict("Email already in use.");

            var created = await _users.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetByid), new { id = created.Id }, created);
        }
    }
}
