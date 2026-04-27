using API.Common;
using API.DTO.Alerts;
using API.Models.Alerts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/alerts")]
    public class AlertController(IAlertService alertService) : ControllerBase
    {
        private readonly IAlertService _alertService = alertService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertResponse>>> GetAllAlertsAsync()
        {
            var alerts = await _alertService.GetAllAlertsAsync();
            return Ok(alerts.Select(ToResponse));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AlertResponse>> GetAlertByIdAsync([FromRoute] Guid id)
        {
            var result = await _alertService.GetAlertByIdAsync(id);

            if (!result.IsSuccess)
                return MapError(result.Error!);

            return Ok(ToResponse(result.Value!));
        }

        [HttpPost]
        public async Task<ActionResult<AlertResponse>> CreateAlertAsync(
            [FromBody] CreateAlertRequest alertRequest
        )
        {
            var result = await _alertService.CreateAlertAsync(alertRequest);
            if (!result.IsSuccess)
                return MapError(result.Error!);

            var alertResponse = ToResponse(result.Value!);
            return CreatedAtAction(
                nameof(GetAlertByIdAsync),
                new { id = alertResponse.Id },
                alertResponse
            );
        }

        [HttpPost("{id:Guid}/publish")]
        public async Task<IActionResult> PublishAlertAsync([FromRoute] Guid id)
        {
            var result = await _alertService.PublishAlertAsync(id);
            if (!result.IsSuccess)
                return MapError(result.Error!);

            return NoContent();
        }

        [HttpPost("{id:Guid}/cancel")]
        public async Task<IActionResult> CancelAlertAsync([FromRoute] Guid id)
        {
            var result = await _alertService.CancelAlertAsync(id);
            if (!result.IsSuccess)
                return MapError(result.Error!);

            return NoContent();
        }

        private static AlertResponse ToResponse(Alert alert) =>
            new()
            {
                Id = alert.Id,
                Message = alert.Message,
                Area = alert.Area,
                Status = alert.Status,
            };

        private ActionResult MapError(Error error) =>
            error.Type switch
            {
                ErrorType.NotFound => NotFound(error.Message),
                ErrorType.Conflict => Conflict(error.Message),
                ErrorType.Validation => BadRequest(error.Message),
                _ => StatusCode(500, error.Message),
            };
    }
}
