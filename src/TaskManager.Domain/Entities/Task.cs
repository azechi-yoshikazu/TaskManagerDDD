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

    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
