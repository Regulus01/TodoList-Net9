using System.Net;
using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskItem;
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
    /// Create one task
    /// </summary>
    /// <param name="partialTaskItemDto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskItemDto partialTaskItemDto)
    {
        var result = _taskItemAppService.CreateTaskItem(partialTaskItemDto);

        return ApiResponse(HttpStatusCode.Created, result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskItemDto taskItemDto)
    {
        var result = _taskItemAppService.UpdateTaskItem(taskItemDto);
        
        return ApiResponse(HttpStatusCode.OK, result);
    }
}