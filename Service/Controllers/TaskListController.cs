using System.ComponentModel.DataAnnotations;
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
        var result = _taskListAppService.Create(taskListDto);

        return Created(string.Empty, result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskListDto taskItemDto)
    {
        var result = _taskListAppService.Update(taskItemDto);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult Get([FromRoute] Guid? id)
    {
        var result = _taskListAppService.Get(id);

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid? id)
    {
        _taskListAppService.Delete(id);
        return NoContent();
    }
}