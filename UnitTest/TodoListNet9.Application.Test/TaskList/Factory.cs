﻿using Application.Dto.TaskItem;
using Application.Dto.TaskList;
using Domain.Commands.Events;

namespace TodoListNet9.Application.Test.TaskList;

public static class Factory
{
    public static CreateTaskItemDto GenerateCreateTaskItemDto(string? title = null, string description = "One description", 
        DateTimeOffset? dueDate = null, Guid? taskListId = null)
    {
        return new CreateTaskItemDto
        {
            Title = title ?? "New task item", 
            Description = description, 
            DueDate = dueDate, 
            TaskListId = taskListId ?? Guid.NewGuid()
        };
    }

    public static CreateTaskListDto GenerateCreateTaskListDto(string title = "", IEnumerable<CreateTaskItemDto>? taskItems = null)
    {
        return new CreateTaskListDto
        {
            Title = title, 
            TaskItems = taskItems
            
        };
    }

    public static CreateTaskListEvent GenerateCreateTaskListEvent(string title = "Title")
    {
        return new CreateTaskListEvent
        {
            Title = title
        };
    }
}