using System.ComponentModel.DataAnnotations;
using System.Net;
using Application.Dto.TaskList;
using Application.Interface;
using Application.ViewModels.TaskList;
using Infra.CrossCutting.Notification.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers.Base;

namespace Service.Controllers;

[Route("api/v1/TaskList")]
public class TaskListController : BaseController
{
    private readonly ITaskListAppService _taskListAppService;

    public TaskListController(ITaskListAppService taskListAppService, INotify notify) : base(notify)
    {
        _taskListAppService = taskListAppService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskListDto taskListDto)
    {
        var result = _taskListAppService.Create(taskListDto);

        return ApiResponse(HttpStatusCode.Created, result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateTaskListDto taskItemDto)
    {
        var result = _taskListAppService.Update(taskItemDto);

        return ApiResponse(HttpStatusCode.OK, result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult Get([FromRoute] Guid? id)
    {
        var result = _taskListAppService.Get(id);

        if (result == null)
            return ApiResponse(HttpStatusCode.NotFound);
        
        return ApiResponse(HttpStatusCode.OK, result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid? id)
    {
        _taskListAppService.Delete(id);
        
        return ApiResponse(HttpStatusCode.NoContent);
    }
}