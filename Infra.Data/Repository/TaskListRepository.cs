using Domain.Interface;
using Infra.Data.Context;
using Infra.Data.Repository.Base;

namespace Infra.Data.Repository;

public class TaskListRepository : BaseRepository<TaskDbContext>, ITaskListRepository 
{
    public TaskListRepository(TaskDbContext context) : base(context) { }
}