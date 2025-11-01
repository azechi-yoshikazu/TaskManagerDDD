using TaskManager.Domain.ValueObjects;
using FluentAssertions;
using TaskManager.Domain.ValueObjects.Tasks;

namespace TaskManager.Domain.Tests.ValueObjects;

public class TaskTitleTests
{
    [Fact]
    public void Create_ShouldSucceed_WhenValidTitle()
    {
        var result = TaskTitle.Create("タイトル");

        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be("タイトル");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldFail_WhenEmptyOrWhitespace(string invalid)
    {
        var result = TaskTitle.Create(invalid);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldBeTrue_WhenValuesAreSame()
    {
        var a = TaskTitle.Create("同じ").Value!;
        var b = TaskTitle.Create("同じ").Value!;

        a.Should().Be(b);
    }
}
