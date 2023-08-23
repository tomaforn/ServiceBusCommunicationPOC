using IntegrationContracts;
using MassTransit;

namespace ServiceBusPOC.Application.TodoItems.EventHandlers
{
    public class WorkorderCreatedEventConsumer : IConsumer<WorkOrderCreatedIntegrationEvent>
    {
        private readonly ILogger<WorkorderCreatedEventConsumer> _logger;

        public WorkorderCreatedEventConsumer(ILogger<WorkorderCreatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<WorkOrderCreatedIntegrationEvent> context)
        {
            _logger.LogInformation("Medusa just created a workorder with details: {@Workorder}", context.Message);
            return Task.CompletedTask;
        }
    }
}
