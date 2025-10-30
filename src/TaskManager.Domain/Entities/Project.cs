using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class Project : AggregateRoot<ProjectId>
{
    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public ProjectStatus Status { get; private set; }

    public UserId OwnerId { get; private set; }

    private List<UserId> _members;
    public IReadOnlyList<UserId> Members => _members.AsReadOnly();

    private List<TaskId> _tasks;
    public IReadOnlyList<TaskId> Tasks => _tasks.AsReadOnly();

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Project(ProjectId id, ProjectName name, UserId ownerId) : base(id)
    {
        Name = name;
        Description = ProjectDescription.Create(string.Empty);
        Status = ProjectStatus.Active;

        OwnerId = ownerId;
        _members = new List<UserId>();
        _tasks = new List<TaskId>();

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Result<Project> Create(string projectName, UserId ownerId)
    {
        var nameResult = ProjectName.Create(projectName);
        if (nameResult.IsFailure)
        {
            return nameResult.Error!;
        }

        var project = new Project(ProjectId.Create(), nameResult.Value!, ownerId);
        project.RaiseDomainEvent(new DomainEvents.ProjectCreatedDomainEvent(project.Id));

        return project;
    }

    public Result UpdateName(string newProjectName)
    {
        var nameResult = ProjectName.Create(newProjectName);
        if (nameResult.IsFailure)
        {
            return nameResult.Error!;
        }

        Name = nameResult.Value!;
        UpdateTimestamp();

        return Result.Success();
    }

    public Result UpdateDescription(string newDescription)
    {
        Description = ProjectDescription.Create(newDescription);
        UpdateTimestamp();

        return Result.Success();
    }

    #region Status Changes
    public Result Complete()
    {
        if (Status != ProjectStatus.Active)
        {
            return ProjectErrors.StatusInvalidTransition;
        }

        Status = ProjectStatus.Completed;

        RaiseDomainEvent(new DomainEvents.ProjectCompletedDomainEvent(Id));
        UpdateTimestamp();

        return Result.Success();
    }

    public Result Archive()
    {
        if (Status != ProjectStatus.Completed)
        {
            return ProjectErrors.StatusInvalidTransition;
        }

        Status = ProjectStatus.Archived;

        RaiseDomainEvent(new DomainEvents.ProjectArchivedDomainEvent(Id));
        UpdateTimestamp();

        return Result.Success();
    }
    #endregion

    #region Member Management
    public Result AddMember(UserId memberId)
    {
        if (Members.Contains(memberId))
        {
            return ProjectErrors.MemberAlreadyExists;
        }

        _members.Add(memberId);
        UpdateTimestamp();

        return Result.Success();
    }
    public Result RemoveMember(UserId memberId)
    {
        if (!_members.Remove(memberId))
        {
            return ProjectErrors.MemberNotFound;
        }

        UpdateTimestamp();

        return Result.Success();
    }
    #endregion

    #region Task Management
    public Result AddTask(TaskId taskId)
    {
        if (Tasks.Contains(taskId))
        {
            return ProjectErrors.TaskAlreadyExists;
        }

        _tasks.Add(taskId);
        UpdateTimestamp();

        return Result.Success();
    }
    public Result RemoveTask(TaskId taskId)
    {
        if (!_tasks.Remove(taskId))
        {
            return ProjectErrors.TaskNotFound;
        }

        UpdateTimestamp();

        return Result.Success();
    }
    #endregion

    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
