using System.Data.Common;

namespace Test.RabbitMq.Shop.Core.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public int OrderId { get; set; }
    public DateTime NotificationDateTime { get; set; }
}