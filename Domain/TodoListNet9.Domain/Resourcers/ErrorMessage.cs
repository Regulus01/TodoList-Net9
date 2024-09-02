namespace Domain.Resourcers;

public static class ErrorMessage
{
    public static (string Code, string Message) SAVE_DATA = ("00x1", "An error ocurred while saving the data.");
    public static (string Code, string Message) UPDATE_DATA = ("00x2", "An error ocurred while updating the data.");
    public static (string Code, string Message) DELETE_DATA = ("00x3", "An error ocurred while deleting the data.");
    public static (string Code, string Message) DATA_NOT_FOUND = ("00x4", "Register not found.");
    public static (string Code, string Message) LINKED_TASKITEM = ("00x5", "Error occured while deleting the tasklist, " +
                                                                           "there are linked task items.");
    public static (string Code, string Message) TITLE_REQUIRED = ("00x6", "Title is required");
    public static (string Code, string Message) DUE_DATE_IN_PAST = ("00x7", "Due date cannot be in the past");
    public static (string Code, string Message) TASK_LIST_NOT_EXIST = ("00x8", "The informated task list doesn't exist.");
    public static (string Code, string Message) NULL_FIELDS = ("00x8", "Fields cannot be null");
    
}