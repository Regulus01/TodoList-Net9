using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskItem;

public class UpdateTaskItemDto
{
    /// <summary>
    /// Id of the task item
    /// </summary>
    [Required(ErrorMessage = "The id field is required.")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Tittle of the task item
    /// </summary>
    [Required(ErrorMessage = "This title is required")]
    [MaxLength(80, ErrorMessage = "Title length cannot be more than 50 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 1 characters")]
    public string Title { get; set; }
    
    /// <summary>
    /// Description of the task item
    /// </summary>
    [MaxLength(150, ErrorMessage = "Description length cannot be more than 150 characters")]
    public string Description { get; set; }
    
    /// <summary>
    /// Date the task will expire
    /// </summary>
    public DateTimeOffset? DueDate { get; set; }
    
    /// <summary>
    /// Status of the task item
    /// </summary>
    /// <list type="bullet">
    /// <item>
    /// <description>True if the task list is completed</description>
    /// </item>
    /// <item>
    /// <description>False if the task list is not completed</description>
    /// </item>
    /// </list>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Id of the task list
    /// </summary>
    [Required(ErrorMessage = "This TaskList id is required")]
    public Guid TaskListId { get; set; }
}