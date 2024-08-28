using Application.Dto.TaskItem;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/TaskItem")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemAppService _taskItemAppService;

    public TaskItemController(ITaskItemAppService taskItemAppService)
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
        
        return Created(string.Empty, result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskItemDto taskItemDto)
    {
        var result = _taskItemAppService.UpdateTaskItem(taskItemDto);
        
        return Ok(result);
    }
}