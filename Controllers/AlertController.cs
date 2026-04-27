using API.DTO.Alerts;
using API.Models.Alerts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("alerts")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpPost]
        public async Task<ActionResult<Alert>> CreateAlertAsync(
            [FromBody] CreateAlertRequest alertRequest
        )
        {
            var created = await _alertService.CreateAlertAsync(alertRequest);
            return CreatedAtAction(nameof(GetAlertByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Alert>> GetAlertByIdAsync([FromRoute] Guid id)
        {
            var alert = await _alertService.GetAlertByIdAsync(id);
            if (alert is null)
                return NotFound();

            return Ok(alert);
        }

        [HttpPost("{id:Guid}/publish")]
        public async Task<IActionResult> PublishAlertAsync([FromRoute] Guid id)
        {
            await _alertService.PublishAlertAsync(id);
            return NoContent();
        }

        [HttpPost("{id:Guid}/cancel")]
        public async Task<IActionResult> CancelAlertAsync([FromRoute] Guid id)
        {
            await _alertService.CancelAlertAsync(id);
            return NoContent();
        }
    }
}
