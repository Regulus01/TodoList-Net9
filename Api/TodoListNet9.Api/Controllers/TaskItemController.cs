using System.Net;
using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskItem;
using Application.ViewModels.TaskList;
using Domain.Interface.Command.Interface;
using Infra.CrossCutting.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/TaskItem")]
public class TaskItemController : BaseController
{
    private readonly ITaskItemAppService _taskItemAppService;

    public TaskItemController(ITaskItemAppService taskItemAppService, INotify notify) : base(notify)
    {
        _taskItemAppService = taskItemAppService;
    }
    
    /// <summary>
    /// Create one task item
    /// </summary>
    /// <param name="partialTaskItemDto"></param>
    /// <returns>The task item as created</returns>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskItemDto partialTaskItemDto)
    {
        var result = _taskItemAppService.CreateTaskItem(partialTaskItemDto);

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Update one task item
    /// </summary>
    /// <param name="taskItemDto"></param>
    /// <returns>The task item updated</returns>
    [ProducesResponseType(typeof(TaskListViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskItemDto taskItemDto)
    {
        var result = _taskItemAppService.UpdateTaskItem(taskItemDto);
        
        return Response(HttpStatusCode.OK, result);
    }
    
    /// <summary>
    /// Get task item for id
    /// </summary>
    /// <param name="id">Task item Id</param>
    /// <returns>Task item</returns>
    [ProducesResponseType(typeof(TaskItemViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult Get([FromRoute] Guid? id)
    {
        var result = _taskItemAppService.Get(id);

        if (result == null)
            return Response(HttpStatusCode.NotFound);
        
        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Delete one task item for id
    /// </summary>
    /// <param name="id">Task item id</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid? id)
    {
        _taskItemAppService.Delete(id);
        
        return Response(HttpStatusCode.NoContent);
    }
}