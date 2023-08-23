using ServiceBusPOC.Domain.Common;
using ServiceBusPOC.Domain.Entities;

namespace ServiceBusPOC.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
