using TaskManager.Domain.Primitives;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class Task : AggregateRoot
{
    public TaskTitle Title { get; private set; }
    public ValueObjects.TaskStatus Status { get; private set; }

    private Task(TaskTitle title, ValueObjects.TaskStatus status)
    {
        Title = title;
        Status = status;
    }

    public static Result<Task> Create(string title, TaskState status)
    {
        var titleResult = TaskTitle.Create(title);
        if (titleResult.IsFailure)
        {
            return titleResult.Error!;
        }
        var statusResult = ValueObjects.TaskStatus.Create(status);
        if (statusResult.IsFailure)
        {
            return statusResult.Error!;
        }
        return new Task(titleResult.Value!, statusResult.Value!);
    }

    public Result UpdateTitle(string newTitle)
    {
        var titleResult = TaskTitle.Create(newTitle);
        if (titleResult.IsFailure)
        {
            return titleResult.Error!;
        }
        Title = titleResult.Value!;
        return Result.Success();
    }

    public Result UpdateStatus(TaskState newStatus)
    {
        var statusResult = ValueObjects.TaskStatus.Create(newStatus);
        if (statusResult.IsFailure)
        {
            return statusResult.Error!;
        }
        Status = statusResult.Value!;
        return Result.Success();
    }
}
