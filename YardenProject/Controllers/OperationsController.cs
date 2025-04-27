using Microsoft.AspNetCore.Mvc;
using YardenProject.Models;
using YardenProject.Services;

namespace YardenProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _service;

        public OperationsController(IOperationService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] OperationRequest request)
        {
            var result = _service.Calculate(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("last-operations/{operation}")]
        public IActionResult GetLastOperations(string operation)
        {
            return Ok(_service.GetLastOperations(operation));
        }

        [HttpGet("monthly-count/{operation}")]
        public IActionResult GetMonthlyCount(string operation)
        {
            return Ok(_service.GetMonthlyOperationCount(operation));
        }
    }
}
