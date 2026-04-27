using API.DTO.Alerts;
using API.Models.Alerts;
using API.Models.Enums;
using API.Repositories;

namespace API.Services
{
    public class AlertService(IAlertRepository repository) : IAlertService
    {
        private readonly IAlertRepository _repository = repository;

        public async Task<IEnumerable<Alert>> GetAllAlertsAsync()
        {
            return await _repository.GetAllAlertsAsync();
        }

        public async Task<Alert> CreateAlertAsync(CreateAlertRequest alertRequest)
        {
            var existingAlert = await _repository.GetByAreaAsync(alertRequest.Area);

            if (existingAlert != null)
            {
                throw new InvalidOperationException(
                    $"An alert for area '{alertRequest.Area}' already exists."
                );
            }

            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                Message = alertRequest.Message,
                Area = alertRequest.Area,
                Status = AlertStatus.Draft,
            };

            return await _repository.CreateAlertAsync(alert);
        }

        public async Task<Alert?> GetAlertByIdAsync(Guid id)
        {
            return await _repository.GetAlertByIdAsync(id);
        }

        public async Task PublishAlertAsync(Guid id)
        {
            var alert =
                await _repository.GetAlertByIdAsync(id)
                ?? throw new KeyNotFoundException($"Alert with id {id} was not found.");

            if (alert.Status != AlertStatus.Draft)
            {
                throw new InvalidOperationException(
                    $"Only alerts in Draft status can be published. Current status: {alert.Status}"
                );
            }

            await _repository.PublishAlertAsync(id);
        }

        public async Task CancelAlertAsync(Guid id)
        {
            var alert =
                await _repository.GetAlertByIdAsync(id)
                ?? throw new KeyNotFoundException($"Alert with id {id} was not found.");

            if (alert.Status != AlertStatus.Published)
            {
                throw new InvalidOperationException(
                    $"Only alerts in Published status can be canceled. Current status: {alert.Status}"
                );
            }

            await _repository.CancelAlertAsync(id);
        }
    }
}
