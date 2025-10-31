using TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;
using FluentAssertions;
using TaskManager.Domain.ValueObjects.Projects;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Tests.Entities;

public class ProjectTests
{
    [Fact]
    public void Create_ShouldInitializeCorrectly()
    {
        var owner = UserId.Create();

        var projectResult = Project.Create("プロジェクトA", owner);
        projectResult.IsSuccess.Should().BeTrue();

        var project = projectResult.Value!;

        project.Name.Value.Should().Be("プロジェクトA");
        project.OwnerId.Should().Be(owner);
        project.Status.Should().Be(ProjectStatus.Active);
    }

    [Fact]
    public void UpdateName_ShouldChangeName_WhenValid()
    {
        var owner = UserId.Create();
        
        var projectResult = Project.Create("古い名前", owner);
        projectResult.IsSuccess.Should().BeTrue();

        var project = projectResult.Value!;

        var result = project.UpdateName("新しい名前");

        result.IsSuccess.Should().BeTrue();
        project.Name.Value.Should().Be("新しい名前");
    }

    [Fact]
    public void Complete_ShouldChangeStatusToCompleted()
    {
        var owner = UserId.Create();
        var projectResult = Project.Create("プロジェクトB", owner);
        projectResult.IsSuccess.Should().BeTrue();

        var project = projectResult.Value!;

        var result = project.Complete();

        result.IsSuccess.Should().BeTrue();
        project.Status.Should().Be(ProjectStatus.Completed);
    }

    [Fact]
    public void Archive_ShouldFail_IfNotCompleted()
    {
        var owner = UserId.Create();
        var projectResult = Project.Create("プロジェクトC", owner);
        projectResult.IsSuccess.Should().BeTrue();
        var project = projectResult.Value!;

        var result = project.Archive();

        result.IsFailure.Should().BeTrue();
    }
}
