using TaskManager.Domain.Primitives;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class Task : AggregateRoot<TaskId>
{
    public TaskTitle Title { get; private set; }
    public TaskDescription Description { get; private set; }
    public ValueObjects.TaskStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DueDate? DueDate { get; private set; }
    public CompletedAt? CompletedAt { get; private set; }

    public UserId? AssignedUserId { get; private set; }
    public ProjectId ProjectId { get; private set; }

    private List<TaskId> _subTasks;
    public IReadOnlyList<TaskId> SubTasks => _subTasks.AsReadOnly();

    private Task(TaskId id, TaskTitle title, ProjectId projectId)
        : base(id)
    {
        Title = title;
        var descriptionResult =TaskDescription.Create(string.Empty);
        Description = descriptionResult.Value!;
        ValueObjects.TaskStatus.Create(TaskState.NotStarted);
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        DueDate = null;
        CompletedAt = null;

        AssignedUserId = null;
        ProjectId = projectId;

        _subTasks = new();

    }

    public static Result<Task> Create(string title, ProjectId projectId)
    {
        var titleResult = TaskTitle.Create(title);
        if (titleResult.IsFailure)
        {
            return titleResult.Error!;
        }

        return new Task(TaskId.Create(), titleResult.Value!, ProjectId.Create());
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
        switch (Status.Value)
        {
            case TaskState.NotStarted:
                if(newStatus != TaskState.InProgress)
                {
                    return DomainErrors.TaskErrors.StatusInvalidTransition;
                }
                break;
            case TaskState.InProgress:
                if(newStatus != TaskState.Completed)
                {
                    return DomainErrors.TaskErrors.StatusInvalidTransition;
                }
            break;
            case TaskState.Completed:
                if(newStatus == TaskState.Completed)
                {
                    return DomainErrors.TaskErrors.StatusInvalidTransition;
                }
                break;
        }

        Status = ValueObjects.TaskStatus.Create(newStatus);

        return Result.Success();
    }
}
