namespace API.DTO.Alerts;

public class AlertResponse
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public required string Area { get; set; }
    public bool IsPublished { get; set; }
}
