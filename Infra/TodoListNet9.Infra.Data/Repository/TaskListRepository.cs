using Domain.Interface;
using Domain.Interface.Repository;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.Extensions.Logging;

namespace Infra.Data.Repository;

public class TaskListRepository : BaseRepository<TaskDbContext, TaskListRepository>, ITaskListRepository 
{
    public TaskListRepository(TaskDbContext context, ILogger<TaskListRepository> logger) : base(context, logger) { }
}