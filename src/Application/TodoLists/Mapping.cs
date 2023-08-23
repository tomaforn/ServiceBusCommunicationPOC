using ServiceBusPOC.WebUI.Shared.TodoLists;

namespace ServiceBusPOC.Application.TodoLists;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
