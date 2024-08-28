using Application.Dto;
using Application.Dto.TaskList;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/TaskList")]
public class TaskListController : ControllerBase
{
    private readonly ITaskListAppService _taskListAppService;

    public TaskListController(ITaskListAppService taskListAppService)
    {
        _taskListAppService = taskListAppService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskListDto taskListDto)
    {
        var result = _taskListAppService.CreateTaskList(taskListDto);
        
        return Created(string.Empty, result);
    }

    [HttpPut]
    public IActionResult UpdateTaskList([FromBody] UpdateTaskListDto taskItemDto)
    {
        var result = _taskListAppService.UpdateTaskList(taskItemDto);
        
        return Ok(result);
    }
}