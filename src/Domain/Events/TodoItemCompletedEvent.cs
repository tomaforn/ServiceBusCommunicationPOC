using ServiceBusPOC.Domain.Common;
using ServiceBusPOC.Domain.Entities;

namespace ServiceBusPOC.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
