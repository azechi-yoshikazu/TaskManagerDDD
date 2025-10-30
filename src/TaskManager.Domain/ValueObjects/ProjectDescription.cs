using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public sealed class ProjectDescription : ValueObject
{
    public string Value { get; }

    private ProjectDescription(string value)
    {
        Value = value;
    }

    public static ProjectDescription Create(string value) => new ProjectDescription(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
