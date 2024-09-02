using Application.Mapper;
using Application.Services;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using TodoListNet9.Application.Test.Base;

namespace TodoListNet9.Application.Test.TaskList;

public class TaskListFixture : BaseFixture
{
    public TaskListAppService GetFixtureTaskList()
    {
        Mocker = new AutoMocker();
        
        Mocker.Use(MappingConfiguration.RegisterMappings().CreateMapper());

        return Mocker.CreateInstance<TaskListAppService>();
    }
}