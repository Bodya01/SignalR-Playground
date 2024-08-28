using SignalRPlayground.Server.Models;

namespace SignalRPlayground.Server.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationToAllUsersAsync(Notification notification, CancellationToken cancellationToken = default);
        Task SendNotificationToGroupAsync(string groupName, Notification notification, CancellationToken cancellationToken = default);
        Task SendNotificationToUserAsync(string userName, Notification notification, CancellationToken cancellationToken = default);
    }
}