using IntegrationContracts;
using MediatR;
using ServiceBusPOC.Application.Common;
using ServiceBusPOC.Domain.Events;
using ServiceBusPOC.WebUI.Shared.TodoItems;

namespace ServiceBusPOC.Application.TodoItems.Commands;

public record CreateTodoItemCommand(CreateTodoItemRequest Item) : IRequest<int>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Item).SetValidator(new CreateTodoItemRequestValidator());
    }
}

public class CreateTodoItemCommandHandler
        : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventBus _eventBus;

    public CreateTodoItemCommandHandler(IApplicationDbContext context, IEventBus eventBus)
    {
        _context = context;
        _eventBus = eventBus;
    }

    public async Task<int> Handle(CreateTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.Item.ListId,
            Title = request.Item.Title,
            Done = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));
        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
