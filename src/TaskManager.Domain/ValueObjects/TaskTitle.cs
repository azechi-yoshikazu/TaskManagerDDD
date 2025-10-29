using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public sealed class TaskTitle : ValueObject
{
    public string Value { get; }

    private TaskTitle(string value)
    {
        Value = value;
    }

    public static Result<TaskTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainErrors.TaskErrors.TitleEmpty;
        }
        if (value.Length > 200)
        {
            return DomainErrors.TaskErrors.TitleTooLong;
        }
        return new TaskTitle(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
