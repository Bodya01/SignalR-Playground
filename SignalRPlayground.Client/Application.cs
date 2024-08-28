using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRPlayground.Client
{
    internal sealed class Application
    {
        private readonly HubConnection _hubConnection;

        public Application()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7244/playground-hub")
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                await Console.Out.WriteLineAsync("Restarting SignalR connection...");
                await Task.Delay(1000);
                await _hubConnection.StartAsync();
                await Console.Out.WriteLineAsync("SignalR connection established!");
            };

            _hubConnection.On<Notification>("NotifyAllClients", HandleAllClientMessage);
            _hubConnection.On<Notification>("NotifyByGroup", HandleGroupMessage);
            _hubConnection.On<Notification>("NotifyByConnectionId", HandleUserMessage);
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Application started!");

            await _hubConnection.StartAsync();

            await JoinGroup("console-client-group");

            Console.ReadLine();
            Console.WriteLine("Application shutting down!");
        }

        private void HandleAllClientMessage(Notification notification) => Console.WriteLine($"Message for all users received: {notification.Message}");

        private void HandleGroupMessage(Notification notification) => Console.WriteLine($"Message for group received: {notification.Message}");

        private void HandleUserMessage(Notification notification) => Console.WriteLine($"Message for user received: {notification.Message}");

        private async Task JoinGroup(string name)
        {
            await Console.Out.WriteLineAsync($"Joining group: \"{name}\"");

            await _hubConnection.InvokeAsync("EnterGroupAsync", name);

            await Console.Out.WriteLineAsync($"Joined group: \"{name}\"");
        }
    }

    public sealed record Notification(Guid Id, string Message /* JSON can be sent as well */);
}
