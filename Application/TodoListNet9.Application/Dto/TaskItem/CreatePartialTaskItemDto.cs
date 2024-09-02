using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskItem;

public class CreatePartialTaskItemDto
{
    /// <summary>
    /// Title of the task item
    /// </summary>
    [Required(ErrorMessage = "This title is required")]
    [MaxLength(80, ErrorMessage = "Title length cannot be more than 50 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 1 characters")]
    public string Title { get; set; }
    
    /// <summary>
    /// Description of the task item
    /// </summary>
    [MaxLength(150, ErrorMessage = "Description length cannot be more than 150 characters")]
    public string Description { get;set; }
    
    /// <summary>
    /// Date the task will expire
    /// </summary>
    public DateTimeOffset? DueDate { get; set; }
}