using Application.Dto.TaskList;
using Application.ViewModels;
using Application.ViewModels.TaskList;

namespace Application.Interface;

/// <summary>
/// Task list interface
/// </summary>
public interface ITaskListAppService
{
    /// <summary>
    /// Create one task item
    /// </summary>
    /// <param name="taskListDto">Data for creation</param>
    /// <returns>Task list has been created</returns>
    TaskListViewModel? Create(CreateTaskListDto? taskListDto);
    
    /// <summary>
    /// Get task list for id
    /// </summary>
    /// <remarks>
    /// The task list obtained, will also bring its task items
    /// </remarks>
    /// <param name="id">Task list id</param>
    /// <returns>Task list has been obtained</returns>
    TaskListViewModel? Get(Guid? id);
    
    /// <summary>
    /// Update one task list
    /// </summary>
    /// <param name="taskListDto">Data for update</param>
    /// <returns>Task list has been updated</returns>
    TaskListViewModel? Update(UpdateTaskListDto taskListDto);
    
    /// <summary>
    /// Delete one task list
    /// </summary>
    /// <remarks>
    ///     A task list cannot be deleted with have task items associated
    /// </remarks>
    /// <param name="id">Task list id</param>
    void Delete(Guid? id);
}