using System.Net;
using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskItem;
using Application.ViewModels.TaskList;
using Infra.CrossCutting.Notification.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers.Base;

namespace Service.Controllers;

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
}