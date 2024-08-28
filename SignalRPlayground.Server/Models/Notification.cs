#nullable disable

namespace SignalRPlayground.Server.Models
{
    public sealed record Notification(Guid Id, string Message /* JSON can be sent as well */);
}
