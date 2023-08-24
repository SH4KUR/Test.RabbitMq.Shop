using Microsoft.AspNetCore.Mvc;
using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(ILogger<NotificationController> logger, INotificationRepository notificationRepository)
    {
        _logger = logger;
        _notificationRepository = notificationRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Notification>> Get()
    {
        return Ok(_notificationRepository
            .GetNotifications()
            .OrderByDescending(n => n.NotificationDateTime));
    }
}