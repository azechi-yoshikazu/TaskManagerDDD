using TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;
using FluentAssertions;
using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Tests.Entities;

using Task = Domain.Entities.Task;
using TaskStatus = Domain.ValueObjects.Tasks.TaskStatus;

public class TaskTests
{
    [Fact]
    public void Create_ShouldSucceed_WithValidInput()
    {
        var projectId = ProjectId.Create();
        var result = Task.Create("タスクA", projectId);

        result.IsSuccess.Should().BeTrue();
        var task = result.Value!;
        task.Title.Value.Should().Be("タスクA");
        task.Status.Should().Be(TaskStatus.NotStarted);
        task.ProjectId.Should().Be(projectId);
    }

    [Fact]
    public void Start_ShouldChangeStatus_FromNotStartedToInProgress()
    {
        var projectId = ProjectId.Create();
        var task = Task.Create("タスクB", projectId).Value!;

        var result = task.Start();

        result.IsSuccess.Should().BeTrue();
        task.Status.Should().Be(TaskStatus.InProgress);
    }

    [Fact]
    public void Start_ShouldFail_WhenAlreadyCompleted()
    {
        var projectId = ProjectId.Create();
        var task = Task.Create("タスクC", projectId).Value!;
        task.Complete();

        var result = task.Start();

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Complete_ShouldSetCompletedAt()
    {
        var projectId = ProjectId.Create();
        var task = Task.Create("タスクD", projectId).Value!;

        task.Start();
        var result = task.Complete();

        result.IsSuccess.Should().BeTrue();
        task.Status.Should().Be(TaskStatus.Completed);
        task.CompletedAt.Should().NotBeNull();
    }

    [Fact]
    public void UpdateDueDate_ShouldSetDueDate_WhenValid()
    {
        var projectId = ProjectId.Create();
        var task = Task.Create("タスクE", projectId).Value!;
        var due = DateTime.UtcNow.AddDays(5);

        var result = task.UpdateDueDate(due);

        result.IsSuccess.Should().BeTrue();
        task.DueDate!.Value.Should().BeCloseTo(due, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void AssignUser_ShouldUpdateUser_WhenChanged()
    {
        var projectId = ProjectId.Create();
        var task = Task.Create("タスクF", projectId).Value!;
        var userId = UserId.Create();

        var result = task.AssignUser(userId);

        result.IsSuccess.Should().BeTrue();
        task.AssignedUserId.Should().Be(userId);
    }
}
