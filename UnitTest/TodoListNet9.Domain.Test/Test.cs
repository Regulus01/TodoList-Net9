using Domain.Resourcers;

namespace TodoListNet9.Test;

public class Test
{
    [Fact(DisplayName = "TaskItemValidate - Validate task item - Success")]
    public void TaskItem_Validatetaskitem_Success()
    {
       //Arrange
       var taskItem = Factory.GenerateTaskItem();
       
       //Act
       var validationResult = taskItem.Validate();
       
       //Assert
       Assert.Equal(true, validationResult.IsValid);
       Assert.Equal(0, validationResult.Erros.Count);
    } 
    
    [Fact(DisplayName = "TaskItemValidate - Task item with errors - Failed")]
    public void TaskItemValidate_TaskItemWithErros_Failed()
    {
        //Arrange
        var oldDate = DateTime.Now.AddDays(-1);
        
        var taskItem = Factory.GenerateTaskItem(" ", dueDate: oldDate);
        
        //Act
        var validationResult = taskItem.Validate();
       
        //Assert
        Assert.Equal(false, validationResult.IsValid);
        Assert.Equal(2, validationResult.Erros.Count);
        Assert.Equal(validationResult.Erros.First().Value, ErrorMessage.TITLE_REQUIRED.Message);
        Assert.Equal(validationResult.Erros.First().Key, ErrorMessage.TITLE_REQUIRED.Code);        
        Assert.Equal(validationResult.Erros.Last().Value, ErrorMessage.DUE_DATE_IN_PAST.Message);
        Assert.Equal(validationResult.Erros.Last().Key, ErrorMessage.DUE_DATE_IN_PAST.Code);
    }
    
    [Fact(DisplayName = "TaskItemValidate - Validate task list - Success")]
    public void TaskListValidate_ValidateTaskList_Success()
    {
        //Arrange
        var taskList = Factory.GenerateTaskList("New task list");
        
        //Act
        var validationResult = taskList.Validate();
       
        //Assert
        Assert.Equal(true, validationResult.IsValid);
        Assert.Equal(0, validationResult.Erros.Count);
    }
    
    [Fact(DisplayName = "TaskItemValidate - Validate with errors - Failed")]
    public void TaskListValidate_ValidateWithErrors_Failed()
    {
        //Arrange
        var taskList = Factory.GenerateTaskList(" ");
        
        //Act
        var validationResult = taskList.Validate();
       
        //Assert
        Assert.Equal(false, validationResult.IsValid);
        Assert.Equal(1, validationResult.Erros.Count);
        Assert.Equal(validationResult.Erros.First().Value, ErrorMessage.TITLE_REQUIRED.Message);
        Assert.Equal(validationResult.Erros.First().Key, ErrorMessage.TITLE_REQUIRED.Code);      
    }
}