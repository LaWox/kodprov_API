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

        public Task PublishAlertAsync(Guid id)
        {
            var alert =
                _alerts.FirstOrDefault(a => a.Id == id)
                ?? throw new KeyNotFoundException($"Alert with id {id} was not found.");
            alert.Status = AlertStatus.Published;
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task CancelAlertAsync(Guid id)
        {
            var alert =
                _alerts.FirstOrDefault(a => a.Id == id)
                ?? throw new KeyNotFoundException($"Alert with id {id} was not found.");
            alert.Status = AlertStatus.Canceled;
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task<Alert?> GetByAreaAsync(string area)
        {
            return Task.FromResult(_alerts.FirstOrDefault(a => a.Area == area));
        }
    }
}
