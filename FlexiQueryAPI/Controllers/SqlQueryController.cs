using FlexiQueryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexiQueryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SqlQueryController : ControllerBase
    {
        private readonly ISqlExecutor _executor;
        private readonly ILogger<SqlQueryController> _logger;

        public SqlQueryController(ISqlExecutor executor, ILogger<SqlQueryController> logger)
        {
            _executor = executor;
            _logger = logger;
        }

        public class SqlRequest
        {
            public string Query { get; set; } = string.Empty;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute([FromBody] SqlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Query))
                return BadRequest("Query cannot be empty.");

            try
            {
                var result = await _executor.ExecuteQueryAsync(request.Query);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Blocked query: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing query.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}
