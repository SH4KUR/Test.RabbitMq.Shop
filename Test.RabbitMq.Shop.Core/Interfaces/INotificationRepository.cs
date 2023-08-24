using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Core.Interfaces;

public interface INotificationRepository
{
    public IEnumerable<Notification> GetNotifications();
    public void AddNotification(Notification notification);
}