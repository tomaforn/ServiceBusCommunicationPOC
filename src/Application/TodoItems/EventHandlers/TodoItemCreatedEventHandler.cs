using IntegrationContracts;
using ServiceBusPOC.Application.Common;
using ServiceBusPOC.Domain.Events;

namespace ServiceBusPOC.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;
    private readonly IEventBus _eventBus;
    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        _eventBus.PublishAsync(
            new TodoItemCreatedIntegrationEvent
            {
                Id = notification.Item.Id,
                Name = notification.Item.Title,
                ListId = notification.Item.ListId
            },
            cancellationToken);

        return Task.CompletedTask;
    }
}
