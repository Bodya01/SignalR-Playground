using Microsoft.AspNetCore.SignalR;
using SignalRPlayground.Server.Hubs;
using SignalRPlayground.Server.Models;
using SignalRPlayground.Server.Services.Interfaces;

namespace SignalRPlayground.Server.Services.Implementations
{
    internal sealed class NotificationService : INotificationService
    {
        private readonly IHubContext<PlaygroundHub> _hub;

        public NotificationService(IHubContext<PlaygroundHub> hub)
        {
            _hub = hub;
        }

        public async Task SendNotificationToAllUsersAsync(Notification notification, CancellationToken cancellationToken = default) =>
            await _hub.Clients.All.SendAsync("NotifyAllClients", notification, cancellationToken);

        public async Task SendNotificationToGroupAsync(string name, Notification notification, CancellationToken cancellationToken = default) =>
            await _hub.Clients.Group(name).SendAsync("NotifyByGroup", notification, cancellationToken);

        public async Task SendNotificationToUserAsync(string userName, Notification notification, CancellationToken cancellationToken = default)
        {
            var user = ConnectionHelper.Connections[userName] ?? throw new Exception("No user");

            await _hub.Clients.Client(user).SendAsync("NotifyByConnectionId", notification, cancellationToken);
        }
    }
}
