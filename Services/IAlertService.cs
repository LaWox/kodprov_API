using API.DTO.Alerts;
using API.Models.Alerts;

namespace API.Services
{
    public interface IAlertService
    {
        Task<Alert> CreateAlertAsync(CreateAlertRequest alertRequest);
        Task<Alert?> GetAlertByIdAsync(Guid id);
        Task PublishAlertAsync(Guid id);
        Task CancelAlertAsync(Guid id);
    }
}
