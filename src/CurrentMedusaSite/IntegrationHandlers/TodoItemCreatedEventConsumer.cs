using IntegrationContracts;
using MassTransit;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CurrentMedusaSite.IntegrationHandlers
{
    public class TodoItemCreatedIntegrationEventConsumer : IConsumer<TodoItemCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<TodoItemCreatedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Debug.WriteLine($"TodoItem created message: {jsonMessage}");
            return Task.CompletedTask;
        }
    }
}
