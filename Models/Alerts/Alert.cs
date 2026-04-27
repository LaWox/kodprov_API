using API.Models.Enums;

namespace API.Models.Alerts
{
    public class Alert
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
        public required string Area { get; set; }
        public AlertStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
