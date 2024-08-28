using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskItem;

public class CreateTaskItemDto : CreatePartialTaskItemDto
{
    [Required(ErrorMessage = "This TaskList id is required")]
    public Guid TaskListId { get; set; }
}