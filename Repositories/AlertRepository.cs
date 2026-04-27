using API.Models.Alerts;
using API.Models.Enums;

namespace API.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private static readonly List<Alert> _alerts = [];

        public Task<IEnumerable<Alert>> GetAllAlertsAsync()
        {
            return Task.FromResult<IEnumerable<Alert>>(_alerts);
        }

        public Task<Alert> CreateAlertAsync(Alert alert)
        {
            var now = DateTime.UtcNow;
            alert.CreatedAt = now;
            alert.UpdatedAt = now;
            _alerts.Add(alert);

            return Task.FromResult(alert);
        }

        public Task<Alert?> GetAlertByIdAsync(Guid id)
        {
            var alert = _alerts.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(alert);
        }

        public Task PublishAlertAsync(Alert alert)
        {
            alert.Status = AlertStatus.Published;
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task CancelAlertAsync(Alert alert)
        {
            alert.Status = AlertStatus.Canceled;
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Alert>> GetByAreaAsync(string area, bool publishedOnly = false)
        {
            var alerts = _alerts.Where(a =>
                a.Area == area && (!publishedOnly || a.Status == AlertStatus.Published)
            );
            return Task.FromResult(alerts);
        }
    }
}
