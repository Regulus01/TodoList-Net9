using Application.Dto.TaskItem;
using Application.Interface;
using Domain.Commands;
using Domain.Commands.Events;
using Domain.Interface.Command;
using Domain.Interface.Notification;
using Domain.Resourcers;
using Moq;
using Xunit;

namespace TodoListNet9.Application.Test.TaskList;

public class Test : IClassFixture<TaskListFixture>
{
    private readonly TaskListFixture _fixture;
    private readonly ITaskListAppService _appService;

    public Test(TaskListFixture fixture)
    {
        _fixture = fixture;
        _appService = _fixture.GetFixtureTaskList();
    }

    [Fact(DisplayName = "Create_TaskList_Success")]
    public void Create_TaskList_Success()
    {
        //Arrange
        var taskName = "new task";
        var taskItems = new List<CreateTaskItemDto> { Factory.GenerateCreateTaskItemDto() };
        var taskList = Factory.GenerateCreateTaskListDto(taskName, taskItems: taskItems);

        var eventResponse = Factory.GenerateCreateTaskListEvent(taskName);
        
        //Setup
        _fixture.SetupCommandWithEvent<CreateTaskListCommand, CreateTaskListEvent?>(eventResponse);
        
        //Act
        var response = _appService.Create(taskList);

        //Assert
        Assert.Equal(taskList.Title, response?.Title);
        
        _fixture.NeverNotifications();
        
        _fixture.Mocker.GetMock<ICommandInvoker>()
            .Verify(x => x.Execute<CreateTaskListCommand, CreateTaskListEvent?>(
                    It.Is<CreateTaskListCommand>(y => y.Title == taskList.Title)),
                Times.Once);
    }
    
    
    [Fact(DisplayName = "Create_TaskList_Failure")]
    public void Create_TaskList_Failure()
    {
        //Act
        var response = _appService.Create(null);

        //Assert
        Assert.Null(response);
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(y => y.Equals(ErrorMessage.NULL_FIELDS.Code)), 
                    It.Is<string>(y => y.Equals(ErrorMessage.NULL_FIELDS.Message))),
                Times.Once);
        
        _fixture.Mocker.GetMock<ICommandInvoker>()
            .Verify(x => x.Execute<CreateTaskListCommand, CreateTaskListEvent?>(
                    It.IsAny<CreateTaskListCommand>()),
                Times.Never);
    }
}