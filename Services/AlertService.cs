using API.Common;
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

        public async Task<Result<Alert>> GetAlertByIdAsync(Guid id)
        {
            var alert = await _repository.GetAlertByIdAsync(id);
            if (alert is null)
                return Result<Alert>.Fail(ErrorType.NotFound, $"Alert with id {id} was not found.");

            return Result<Alert>.Ok(alert);
        }

        public async Task<Result<Alert>> CreateAlertAsync(CreateAlertRequest alertRequest)
        {
            var conflictingAlerts = await _repository.GetByAreaAsync(
                alertRequest.Area,
                publishedOnly: true
            );

            if (conflictingAlerts.Any())
                return Result<Alert>.Fail(
                    ErrorType.Conflict,
                    $"An alert for area '{alertRequest.Area}' already exists."
                );

            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                Message = alertRequest.Message,
                Area = alertRequest.Area,
                Status = AlertStatus.Draft,
            };

            return Result<Alert>.Ok(await _repository.CreateAlertAsync(alert));
        }

        public async Task<Result> PublishAlertAsync(Guid id)
        {
            var alert = await _repository.GetAlertByIdAsync(id);

            if (alert is null)
                return Result.Fail(ErrorType.NotFound, $"Alert with id {id} was not found.");

            if (alert.Status != AlertStatus.Draft)
                return Result.Fail(
                    ErrorType.Validation,
                    $"Only alerts in Draft status can be published. Current status: {alert.Status}"
                );

            await _repository.PublishAlertAsync(alert);
            return Result.Ok();
        }

        public async Task<Result> CancelAlertAsync(Guid id)
        {
            var alert = await _repository.GetAlertByIdAsync(id);
            if (alert is null)
                return Result.Fail(ErrorType.NotFound, $"Alert with id {id} was not found.");

            if (alert.Status != AlertStatus.Published)
                return Result.Fail(
                    ErrorType.Validation,
                    $"Only alerts in Published status can be canceled. Current status: {alert.Status}"
                );

            await _repository.CancelAlertAsync(alert);
            return Result.Ok();
        }
    }
}
