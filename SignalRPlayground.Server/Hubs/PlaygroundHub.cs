using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRPlayground.Server.Hubs
{
    public sealed class PlaygroundHub : Hub
    {
        [AllowAnonymous]
        public async Task EnterGroupAsync(string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine($"User {Context.ConnectionId} joined group {groupName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user {Context.ConnectionId} to group {groupName}: {ex.Message}");
                throw; // Re-throw the exception to ensure the client receives it
            }
        }

        [AllowAnonymous]
        public async Task LeaveGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task OnConnectedAsync()
        {
            ConnectionHelper.Connections.Add("Console client", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }

    // To avoid auth implementation
    public static class ConnectionHelper
    {
        public static readonly Dictionary<string, string> Connections = [];
    }
}