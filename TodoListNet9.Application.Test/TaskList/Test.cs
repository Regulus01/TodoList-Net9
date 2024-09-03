using Application.Dto.TaskItem;
using Application.Interface;
using Domain.Interface;
using Domain.Resourcers;
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

        _fixture.NeverNotifications();

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.Add(It.Is<TaskListDomain>(a => a.Title.Equals(taskList.Title))),
                Times.Once());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.SaveChanges(),
                Times.Once());
    }

    [Fact(DisplayName = "Create_TaskErrorValidateTaskItem_Failure")]
    public void Create_TaskListErrorValidateTaskItem_Failure()
    {
        //Arrange
        var taskItems = new List<CreateTaskItemDto>
        {
            Factory.GenerateCreateTaskItemDto(" ",
                dueDate: DateTimeOffset.UtcNow.AddDays(-1))
        };

        var taskList = Factory.GenerateCreateTaskListDto("new task", taskItems: taskItems);

        //Setup
        _fixture.SetupHasNotification(true);

        //Act
        var response = _appService.Create(taskList);

        //Assert
        Assert.Equal(null, response);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.Is<IDictionary<string, string>>(y =>
                    y.First().Value.Equals(ErrorMessage.TITLE_REQUIRED.Message) &&
                    y.Last().Value.Equals(ErrorMessage.DUE_DATE_IN_PAST.Message) &&
                    y.Count == 2)),
                Times.Once);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.Add(It.IsAny<TaskListDomain>()),
                Times.Never());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.SaveChanges(),
                Times.Never());
    }

    [Fact(DisplayName = "Create_TaskErrorValidate_Failure")]
    public void Create_TaskListErrorValidate_Failure()
    {
        //Arrange
        var taskList = Factory.GenerateCreateTaskListDto(" ");

        //Act
        var response = _appService.Create(taskList);

        //Assert
        Assert.Equal(null, response);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.Is<IDictionary<string, string>>(y =>
                    y.First().Value.Equals(ErrorMessage.TITLE_REQUIRED.Message) &&
                    y.Count == 1)),
                Times.Once);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.Add(It.IsAny<TaskListDomain>()),
                Times.Never());

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.SaveChanges(),
                Times.Never());
    }

    [Fact(DisplayName = "Create_TaskSaveChangesError_Failure")]
    public void Create_TaskSaveChanges_Error()
    {
        //Arrange
        var taskList = Factory.GenerateCreateTaskListDto("new task");

        //Setup
        _fixture.SetupSaveChanges<ITaskListRepository>(false);

        //Act
        var response = _appService.Create(taskList);

        //Assert
        Assert.Equal(null, response);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<IDictionary<string, string>>()),
                Times.Never);

        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.Add(It.Is<TaskListDomain>(a => a.Title.Equals(taskList.Title))),
                Times.Once());
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(y => y.Equals(ErrorMessage.SAVE_DATA.Code)),
                    It.Is<string>(y => y.Equals(ErrorMessage.SAVE_DATA.Message))),
                Times.Once());
        
        _fixture.Mocker.GetMock<ITaskListRepository>()
            .Verify(x => x.SaveChanges(),
                Times.Once());
    }
}