using API.Common;
using API.DTO.Alerts;
using API.Models.Alerts;

namespace API.Services
{
    public interface IAlertService
    {
        Task<IEnumerable<Alert>> GetAllAlertsAsync();
        Task<Result<Alert>> GetAlertByIdAsync(Guid id);
        Task<Result<Alert>> CreateAlertAsync(CreateAlertRequest alertRequest);
        Task<Result> PublishAlertAsync(Guid id);
        Task<Result> CancelAlertAsync(Guid id);
    }
}
