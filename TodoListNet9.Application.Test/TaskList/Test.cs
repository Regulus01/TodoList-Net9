using Application.Dto.TaskItem;
using Application.Interface;
using Application.ViewModels.TaskList;
using AutoMapper;
using Domain.Interface;
using Infra.CrossCutting.Notification.Interface;
using Moq;
using Xunit;
using TaskListDomain = Domain.Entities.TaskList;

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
        var taskItems = new List<CreateTaskItemDto> { Factory.GenerateCreateTaskItemDto() };
        var taskList = Factory.GenerateCreateTaskListDto("new task", taskItems: taskItems);
        
        //Setup
        _fixture.SetupSaveChanges<ITaskListRepository>();
        
        //Act
        var response = _appService.Create(taskList);

        //Assert
        Assert.Equal(taskList.Title, response?.Title);
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());
        
        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.Add(It.Is<TaskListDomain>(a => a.Title.Equals(taskList.Title))), 
                Times.Once());
        
        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), 
                Times.Once());
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
    }
}