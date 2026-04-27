using API.Models.Alerts;

namespace API.Repositories
{
    public interface IAlertRepository
    {
        Task<Alert> CreateAlertAsync(Alert alert);
        Task<Alert?> GetByAreaAsync(string area);
        Task<Alert?> GetAlertByIdAsync(Guid id);
        Task PublishAlertAsync(Guid id);
        Task CancelAlertAsync(Guid id);
    }
}
