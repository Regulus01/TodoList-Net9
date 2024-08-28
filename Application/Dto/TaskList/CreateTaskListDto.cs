using System.ComponentModel.DataAnnotations;
using Application.Dto.TaskItem;

namespace Application.Dto.TaskList;

public class CreateTaskListDto
{
    [Required(ErrorMessage = "This title is required")]
    [MaxLength(80, ErrorMessage = "Title length cannot be more than 50 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 1 characters")]
    public string Title { get; set; }
    
    public IEnumerable<CreatePartialTaskItemDto>? TaskItems { get; set; }
}