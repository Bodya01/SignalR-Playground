using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRPlayground.Client
{
    internal sealed class SignalRClient
    {
        private readonly HubConnection _hubConnection;

        public SignalRClient(string url)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
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
            _hubConnection.Closed += async (error) =>
            {
                await HandleConnectionClosedAsync();
            };

            _hubConnection.On<Notification>("NotifyAllClients", HandleAllClientMessage);
            _hubConnection.On<Notification>("NotifyByGroup", HandleGroupMessage);
            _hubConnection.On<Notification>("NotifyByConnectionId", HandleUserMessage);
        }

        private async Task HandleConnectionClosedAsync()
        {
            await Console.Out.WriteLineAsync("Restarting SignalR connection...");

            await Task.Delay(1000);
            await _hubConnection.StartAsync();

            await Console.Out.WriteLineAsync("SignalR connection established!");
        }

        private void HandleAllClientMessage(Notification notification) => Console.WriteLine($"Message for all users received: {notification.Message}");

        private void HandleGroupMessage(Notification notification) => Console.WriteLine($"Message for group received: {notification.Message}");

        private void HandleUserMessage(Notification notification) => Console.WriteLine($"Message for user received: {notification.Message}");
    }
}