using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRPlayground.Client
{
    internal sealed class SignalRClient
    {
        private readonly HubConnection _hubConnection;

        public SignalRClient(string url)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(url, nameof(url));

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            ConfigureHubConnectionEvents();
        }

        public async Task StartAsync() => await _hubConnection.StartAsync();

        public async Task JoinGroupAsync(string groupName)
        {
            await Console.Out.WriteLineAsync($"Joining group: \"{groupName}\"");
            await _hubConnection.InvokeAsync("EnterGroupAsync", groupName);
            await Console.Out.WriteLineAsync($"Joined group: \"{groupName}\"");
        }

        private void ConfigureHubConnectionEvents()
        {
            _hubConnection.Reconnecting += async (error) =>
            {
                await Console.Out.WriteLineAsync("SignalR connection is reconnecting...");
            };

            _hubConnection.Reconnected += async (connectionId) =>
            {
                await Console.Out.WriteLineAsync("SignalR connection reestablished.");
            };

            _hubConnection.Closed += async (error) =>
            {
                await Console.Out.WriteLineAsync("SignalR connection closed. Will attempt to reconnect.");
            };

            _hubConnection.On<Notification>("NotifyAllClients", HandleAllClientMessage);
            _hubConnection.On<Notification>("NotifyByGroup", HandleGroupMessage);
            _hubConnection.On<Notification>("NotifyByConnectionId", HandleUserMessage);
        }

        private void HandleAllClientMessage(Notification notification) =>
            Console.WriteLine($"Message for all users received: {notification.Message}");

        private void HandleGroupMessage(Notification notification) =>
            Console.WriteLine($"Message for group received: {notification.Message}");

        private void HandleUserMessage(Notification notification) =>
            Console.WriteLine($"Message for user received: {notification.Message}");
    }
}