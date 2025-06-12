using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationService _integrations;

        public IntegrationController(IIntegrationService integrations)
        {
            _integrations = integrations;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var list = await _integrations.GetByUserAsync(userId);
            return Ok(list);
        }

        [HttpGet("user/{userId}/provider/{provider}")]
        public async Task<IActionResult> GetByProvider(int userId, string provider)
        {
            var integration = await _integrations.GetByProviderAsync(userId, provider);
            if (integration == null) return NotFound();
            return Ok(integration);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Integration model)
        {
            var created = await _integrations.CreateAsync(model);
            return Ok(created);
        }
    }
}
