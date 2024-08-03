using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbHealthController : ControllerBase
    {
        private readonly DatabaseHealthCheckService _healthCheckService;

        public DbHealthController(DatabaseHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            if (await _healthCheckService.CanConnectAsync())
            {
                return Ok(new { status = "Healthy" });
            }
            else
            {
                return StatusCode(503, new { status = "Unhealthy" });
            }
        }
    }
}
