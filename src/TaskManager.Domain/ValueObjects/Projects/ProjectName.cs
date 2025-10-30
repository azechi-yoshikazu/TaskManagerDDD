using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Projects;

public sealed class ProjectName : ValueObject
{
    public string Value { get; }

    private ProjectName(string value)
    {
        Value = value;
    }

    public static Result<ProjectName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ProjectErrors.NameEmpty;
        }
        if (value.Length > 100)
        {
            return ProjectErrors.NameTooLong;
        }

        return new ProjectName(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
