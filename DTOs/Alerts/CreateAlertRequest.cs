namespace API.DTO.Alerts;

public class CreateAlertRequest
{
    public required string Message { get; set; }
    public required string Area { get; set; }
}
