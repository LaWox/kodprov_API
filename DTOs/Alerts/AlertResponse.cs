using API.Models.Enums;

namespace API.DTO.Alerts;

public class AlertResponse
{
    public Guid Id { get; set; }
    public required string Message { get; set; }
    public required string Area { get; set; }
    public AlertStatus Status { get; set; }
}
