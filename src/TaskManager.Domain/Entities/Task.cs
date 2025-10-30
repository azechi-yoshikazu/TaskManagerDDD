using TaskManager.Domain.DomainErrors;
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

    public bool Expired => 
        DueDate is not null && 
        DueDate.Value < DateTime.UtcNow &&
        Status != ValueObjects.TaskStatus.Completed;

    private Task(TaskId id, TaskTitle title, ProjectId projectId)
        : base(id)
    {
        Title = title;
        var descriptionResult =TaskDescription.Create(string.Empty);
        Description = descriptionResult.Value!;
        Status = ValueObjects.TaskStatus.NotStarted;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        DueDate = null;
        CompletedAt = null;

        AssignedUserId = null;
        ProjectId = projectId;
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
        UpdateTimestamp();


        return Result.Success();
    }

    public Result UpdateDescription(string newDescription)
    {
        var descriptionResult = TaskDescription.Create(newDescription);
        if (descriptionResult.IsFailure)
        {
            return descriptionResult.Error!;
        }

        Description = descriptionResult.Value!;
        UpdateTimestamp();

        return Result.Success();
    }

    #region Status Transition
    public Result Start()
    {
        if (Status != ValueObjects.TaskStatus.NotStarted)
        {
            return DomainErrors.TaskErrors.StatusInvalidTransition;
        }

        Status = ValueObjects.TaskStatus.InProgress;
        UpdateTimestamp();

        return Result.Success();
    }

    public Result Complete()
    {
        if (Status == ValueObjects.TaskStatus.Completed)
        {
            return DomainErrors.TaskErrors.StatusAlreadyCompleted;
        }

        Status = ValueObjects.TaskStatus.Completed;
        CompletedAt = CompletedAt.Create();
        UpdateTimestamp();

        return Result.Success();
    }

    public Result ReOpen(ValueObjects.TaskStatus newStatus)
    {
        if (Status != ValueObjects.TaskStatus.Completed)
        {
            return DomainErrors.TaskErrors.StatusInvalidTransition;
        }

        if(newStatus == ValueObjects.TaskStatus.Completed)
        {
            return DomainErrors.TaskErrors.StatusInvalidTransition;
        }

        Status = newStatus;
        CompletedAt = null;
        UpdateTimestamp();


        return Result.Success();
    }
    #endregion

    public Result UpdateDueDate(DateTime? newDueDate)
    {
        if(Status == ValueObjects.TaskStatus.Completed)
        {
            return TaskErrors.DueDateAlreadyCompleted;
        }

        DueDate = newDueDate is not null ? DueDate.Create(newDueDate.Value) : null;
        UpdateTimestamp();
        return Result.Success();
    }

    public Result AssignUser(UserId? userId)
    {
        if (AssignedUserId == userId)
        {
            return TaskErrors.NoChanged;
        }

        AssignedUserId = userId;
        
        UpdateTimestamp();
        return Result.Success();
    }

    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
