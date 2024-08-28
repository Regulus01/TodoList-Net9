using Domain.Interface;
using Infra.Data.Context;
using Infra.Data.Repository.Base;

namespace Infra.Data.Repository;

public class TaskItemRepository : BaseRepository<TaskDbContext>, ITaskListRepository, ITaskItemRepository
{
    public TaskItemRepository(TaskDbContext context) : base(context) { }
}