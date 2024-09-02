using System.Net;
using Api.Controllers.Base;
using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels.TaskList;
using Infra.CrossCutting.Notification.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/TaskList")]
public class TaskListController : BaseController
{
    private readonly ITaskListAppService _taskListAppService;

    public TaskListController(ITaskListAppService taskListAppService, INotify notify) : base(notify)
    {
        _taskListAppService = taskListAppService;
    }

    /// <summary>
    /// Create Task List
    /// </summary>
    /// <param name="taskListDto"></param>
    /// <returns>The task list a created</returns>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskListDto taskListDto)
    {
        var result = _taskListAppService.Create(taskListDto);

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Update one task list 
    /// </summary>
    /// <param name="taskItemDto"></param>
    /// <returns>The task list with updated attributes</returns>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskListDto taskItemDto)
    {
        var result = _taskListAppService.Update(taskItemDto);

        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Get task list for id
    /// </summary>
    /// <remarks>
    /// The task list obtained, will also bring its task items
    /// </remarks>
    /// <param name="id">Task list Id</param>
    /// <returns>Task list and yours task items</returns>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult Get([FromRoute] Guid? id)
    {
        var result = _taskListAppService.Get(id);

        if (result == null)
            return Response(HttpStatusCode.NotFound);
        
        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Delete one task list for id
    /// </summary>
    /// <remarks>
    ///     A task list cannot be deleted with have task items associated
    /// </remarks>
    /// <param name="id">Id for tak list</param>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid? id)
    {
        _taskListAppService.Delete(id);
        
        return Response(HttpStatusCode.NoContent);
    }
}