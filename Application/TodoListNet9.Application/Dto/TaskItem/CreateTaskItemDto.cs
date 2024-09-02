using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskItem;

public class CreateTaskItemDto : CreatePartialTaskItemDto
{
    /// <summary>
    /// Id of the task list
    /// </summary>
    [Required(ErrorMessage = "This TaskList id is required")]
    public Guid TaskListId { get; set; }
}