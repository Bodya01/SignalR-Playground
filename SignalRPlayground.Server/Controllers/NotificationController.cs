using Microsoft.AspNetCore.Mvc;
using SignalRPlayground.Server.Models;
using SignalRPlayground.Server.Services.Interfaces;

namespace SignalRPlayground.Server.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _notificationService;

        public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet("notify-all")]
        public async Task<IActionResult> SendNotificationToAll(string message, CancellationToken cancellationToken)
        {
            await _notificationService.SendNotificationToAllUsersAsync(new Notification(Guid.NewGuid(), message), cancellationToken);
            return Ok();
        }

        [HttpGet("notify-group")]
        public async Task<IActionResult> SendNotificationToGroup(string groupName, string message, CancellationToken cancellationToken)
        {
            await _notificationService.SendNotificationToGroupAsync(groupName, new Notification(Guid.NewGuid(), message), cancellationToken);
            return Ok();
        }

        [HttpGet("notify-user")]
        public async Task<IActionResult> SendNotificationToUser(string userName, string message, CancellationToken cancellationToken)
        {
            await _notificationService.SendNotificationToUserAsync(userName, new Notification(Guid.NewGuid(), message), cancellationToken);
            return Ok();
        }
    }
}
