using API.Models.Alerts;

namespace API.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private static readonly List<Alert> _alerts = [];

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
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task CancelAlertAsync(Guid id)
        {
            var alert =
                _alerts.FirstOrDefault(a => a.Id == id)
                ?? throw new KeyNotFoundException($"Alert with id {id} was not found.");
            alert.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
