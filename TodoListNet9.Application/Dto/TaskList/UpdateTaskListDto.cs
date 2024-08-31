using System.ComponentModel.DataAnnotations;

namespace Application.Dto.TaskList;

public class UpdateTaskListDto
{
    /// <summary>
    /// Id of the task list
    /// </summary>
    [Required(ErrorMessage = "A Id is required")]
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Title of the task list
    /// </summary>
    [Required(ErrorMessage = "This title is required")]
    [MaxLength(80, ErrorMessage = "Title length cannot be more than 50 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 1 characters")]
    public string Title { get; set; }
}