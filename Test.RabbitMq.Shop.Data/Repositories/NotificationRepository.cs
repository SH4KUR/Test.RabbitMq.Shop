using Test.RabbitMq.Shop.Core.Entities;
using Test.RabbitMq.Shop.Core.Interfaces;

namespace Test.RabbitMq.Shop.Data.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly TestShopContext _context;

    public NotificationRepository(TestShopContext context)
    {
        _context = context;
    }

    public IEnumerable<Notification> GetNotifications()
    {
        return _context.Notifications;
    }

    public void AddNotification(Notification notification)
    {
        _context.Notifications.Add(notification);
        _context.SaveChanges();
    }
}