﻿using Ardalis.GuardClauses;
using ServiceBusPOC.Application.TodoItems.Commands;
using ServiceBusPOC.Application.TodoLists.Commands;
using ServiceBusPOC.Domain.Entities;
using ServiceBusPOC.Domain.Enums;
using ServiceBusPOC.WebUI.Shared.TodoItems;
using ServiceBusPOC.WebUI.Shared.TodoLists;

namespace ServiceBusPOC.Application.SubcutaneousTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemCommand(
            new UpdateTodoItemRequest { Id = 99, Title = "New Title" });

        await FluentActions.Invoking(() => SendAsync(command)).Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "New List" }));

        var itemId = await SendAsync(new CreateTodoItemCommand(
            new CreateTodoItemRequest
            {
                ListId = listId,
                Title = "New Item"
            }));

        var command = new UpdateTodoItemCommand(
            new UpdateTodoItemRequest
            {
                Id = itemId,
                ListId = listId,
                Title = "Updated Item",
                Note = "This is the note.",
                Priority = (int)PriorityLevel.High
            });

        await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.Item.ListId);
        item!.Title.Should().Be(command.Item.Title);
        item.Note.Should().Be(command.Item.Note);
        item.Priority.Should().Be((PriorityLevel)command.Item.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModifiedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
