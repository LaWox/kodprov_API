using API.Models.Alerts;

namespace API.Repositories
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert>> GetAllAlertsAsync();
        Task<Alert> CreateAlertAsync(Alert alert);
        Task<IEnumerable<Alert>> GetByAreaAsync(string area, bool publishedOnly = true);
        Task<Alert?> GetAlertByIdAsync(Guid id);
        Task PublishAlertAsync(Alert alert);
        Task CancelAlertAsync(Alert alert);
    }
}
