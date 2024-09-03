using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRPlayground.Server.Hubs
{
    public sealed class PlaygroundHub : Hub
    {
        [AllowAnonymous]
        public async Task EnterGroupAsync(string groupName)
        {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine($"User {Context.ConnectionId} joined group {groupName}");

        }

        [AllowAnonymous]
        public async Task LeaveGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"User {Context.ConnectionId} left the group {groupName}");
        }

        public override Task OnConnectedAsync()
        {
            ConnectionHelper.Connections.Add("Console client", Context.ConnectionId); // only one client expected
            return base.OnConnectedAsync();
        }
    }

    // To avoid auth implementation
    public static class ConnectionHelper
    {
        public static readonly Dictionary<string, string> Connections = [];
    }
}