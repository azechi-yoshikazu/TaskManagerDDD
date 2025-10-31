using TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;
using FluentAssertions;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void Create_ShouldSucceed_WhenValidValues()
    {
        var result = User.Create("たなか", "tanaka@example.com", Role.Member);

        result.IsSuccess.Should().BeTrue();
        var user = result.Value!;

        user.DisplayName.Value.Should().Be("たなか");
        user.Email.Value.Should().Be("tanaka@example.com");
        user.Role.Should().Be(Role.Member);
        user.Active.Should().BeTrue();
    }

    [Fact]
    public void UpdateDisplayName_ShouldChangeValue()
    {
        var user = User.Create("たなか", "tanaka@example.com", Role.Member).Value!;

        var result = user.UpdateDisplayName("たろう");

        result.IsSuccess.Should().BeTrue();
        user.DisplayName.Value.Should().Be("たろう");
    }

    [Fact]
    public void Deactivate_ShouldSetActiveFalse()
    {
        var user = User.Create("たなか", "tanaka@example.com", Role.Member).Value!;

        user.Deactivate();

        user.Active.Should().BeFalse();
    }
}
