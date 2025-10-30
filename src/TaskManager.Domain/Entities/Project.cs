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

    public static Project Create(ProjectName name, UserId ownerId)
    {
        return new Project(ProjectId.Create(), name, ownerId);
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
        UpdateTimestamp();

        return Result.Success();
    }
    #endregion

    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
