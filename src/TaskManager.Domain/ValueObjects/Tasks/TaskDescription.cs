using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Tasks;

public sealed class TaskDescription : ValueObject
{
    public string Value { get; }

    private TaskDescription(string value)
    {
        Value = value;
    }

    public static Result<TaskDescription> Create(string value)
    {
        if (value.Length > 1000)
        {
            return DomainErrors.TaskErrors.DescriptionTooLong;
        }

        return new TaskDescription(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
