using Microsoft.AspNetCore.Authorization;
using FlexiQueryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FlexiQueryAPI.Interfaces;

namespace FlexiQueryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SqlQueryController : ControllerBase
    {
        private readonly ISqlExecutor _executor;
        private readonly IQueryBuilder _queryBuilder;
        private readonly ILogger<SqlQueryController> _logger;

        public SqlQueryController(ISqlExecutor executor, IQueryBuilder queryBuilder, ILogger<SqlQueryController> logger)
        {
            _executor = executor;
            _queryBuilder = queryBuilder;
            _logger = logger;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] Insert dto)
        {
            try
            {
                var (sql, parameters) = _queryBuilder.BuildInsert(dto);
                var affectedRows = await _executor.ExecuteNonQueryAsync(sql, parameters);
                return Ok(new { affectedRows });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting record.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Update dto)
        {
            try
            {
                var (sql, parameters) = _queryBuilder.BuildUpdate(dto);
                var affectedRows = await _executor.ExecuteNonQueryAsync(sql, parameters);
                return Ok(new { affectedRows });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating record.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] Delete dto)
        {
            try
            {
                var (sql, parameters) = _queryBuilder.BuildDelete(dto);
                var affectedRows = await _executor.ExecuteNonQueryAsync(sql, parameters);
                return Ok(new { affectedRows });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting record.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }

        [HttpPost("select")]
        public async Task<IActionResult> Select([FromBody] Select dto)
        {
            try
            {
                var (sql, parameters) = _queryBuilder.BuildSelect(dto);
                var result = await _executor.ExecuteQueryAsync(sql, parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error selecting records.");
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}