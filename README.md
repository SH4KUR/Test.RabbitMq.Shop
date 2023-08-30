# Test.RabbitMq.Shop

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)

<h2>How to run</h2>

<h4>local startup:</h4>

1. Run **RabbitMQ** from Docker: <code>docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management</code>
2. Run Web API project **Test.RabbitMq.Shop.Api**
3. Run Web Console App **Test.RabbitMq.Shop.Common.StateMachineService**
4. Run Web Console App **Test.RabbitMq.Shop.Common.NotificationService**
5. Run Angular application **Test.RabbitMq.Shop.ClientApp/test-rabbitmq-shop-ui**
6. Open http://localhost:4200
