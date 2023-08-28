# Test.RabbitMq.Shop

<h2>How to run</h2>

1. Run **Docker** with **RabbitMQ**

<code>docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management</code>

2. Run **Test.RabbitMq.Shop.Api**
3. Run **Test.RabbitMq.Shop.Common.NotificationService**
4. Run **Test.RabbitMq.Shop.Common.StateMachineService**
