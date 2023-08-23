// See https://aka.ms/new-console-template for more information
using IntegrationContracts;
using MassTransit;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
//{
//    cfg.Host(new Uri("amqp://rabbitmq:5672"), h =>
//    {
//        h.Username("guest");
//        h.Password("guest");
//    });

//    cfg.ReceiveEndpoint("workorder-created-event", e =>
//    {
//        e.Consumer<WorkOrderCreatedIntegrationEventConsumer>();
//    });
//});

//await busControl.StartAsync(new CancellationToken());

//try
//{
//    Console.WriteLine("Press enter to exit");

//    await Task.Run(() => Console.ReadLine());
//}
//finally
//{
//    await busControl.StopAsync();
//}


class WorkOrderCreatedIntegrationEventConsumer : IConsumer<WorkOrderCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<WorkOrderCreatedIntegrationEvent> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"TodoItem created message: {jsonMessage}");
    }
}
