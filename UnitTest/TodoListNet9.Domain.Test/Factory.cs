using Domain.Entities;

namespace TodoListNet9.Test;

public static class Factory
{
    public static TaskItem GenerateTaskItem(string? title = null, string description = "One description", 
        DateTimeOffset? dueDate = null, Guid? taskListId = null)
    {
        return new TaskItem(title ?? "New task item", description, dueDate, taskListId ?? Guid.NewGuid());
    }

    public static TaskList GenerateTaskList(string title = "", IEnumerable<TaskItem>? taskItems = null)
    {
        return new TaskList(title, taskItems);
    }
}