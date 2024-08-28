using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskItem;

public class CreatePartialTaskItemDto
{
    [Required(ErrorMessage = "This title is required")]
    [MaxLength(80, ErrorMessage = "Title length cannot be more than 50 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 1 characters")]
    public string Title { get; set; }
    
    [MaxLength(150, ErrorMessage = "Description length cannot be more than 150 characters")]
    public string Description { get;set; }
    
    public DateTimeOffset? DueDate { get; set; }
}