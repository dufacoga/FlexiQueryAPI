using FlexiQueryAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("execute")]
        public async Task<IActionResult> ExecuteSelect([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty.");

            if (!query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only SELECT queries are allowed in GET.");

            try
            {
                var result = await _executor.ExecuteQueryAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing SELECT query.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteInsert([FromBody] SqlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Query))
                return BadRequest("Query cannot be empty.");

            if (!request.Query.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only INSERT queries are allowed in POST.");

            try
            {
                var result = await _executor.ExecuteNonQueryAsync(request.Query);
                return Ok(new { affectedRows = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing INSERT query.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpPut("execute")]
        public async Task<IActionResult> ExecuteUpdate([FromBody] SqlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Query))
                return BadRequest("Query cannot be empty.");

            if (!request.Query.TrimStart().StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only UPDATE queries are allowed in PUT.");

            try
            {
                var result = await _executor.ExecuteNonQueryAsync(request.Query);
                return Ok(new { affectedRows = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing UPDATE query.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpDelete("execute")]
        public async Task<IActionResult> ExecuteDelete([FromBody] SqlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Query))
                return BadRequest("Query cannot be empty.");

            if (!request.Query.TrimStart().StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only DELETE queries are allowed in DELETE.");

            try
            {
                var result = await _executor.ExecuteNonQueryAsync(request.Query);
                return Ok(new { affectedRows = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing DELETE query.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}