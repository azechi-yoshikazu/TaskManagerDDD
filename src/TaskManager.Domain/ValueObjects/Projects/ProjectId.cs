using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Projects;

public class ProjectId : ValueObject
{
    public Guid Value { get; }

    private ProjectId(Guid value)
    {
        Value = value;
    }

    public static ProjectId Create() => new ProjectId(Guid.NewGuid());

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
