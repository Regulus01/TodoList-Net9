using Domain.Interface.Repository;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.Extensions.Logging;

namespace Infra.Data.Repository;

public class TaskItemRepository : BaseRepository<TaskDbContext, TaskItemRepository>, ITaskItemRepository
{
    public TaskItemRepository(TaskDbContext context, ILogger<TaskItemRepository> logger) : base(context, logger) { }
}