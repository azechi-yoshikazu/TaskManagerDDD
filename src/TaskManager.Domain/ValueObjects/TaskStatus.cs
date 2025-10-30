using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public enum TaskState
{
    NotStarted,
    InProgress,
    Completed,
}

public sealed class TaskStatus : ValueObject
{
    public TaskState Value { get; }

    private TaskStatus(TaskState value)
    {
        Value = value;
    }

    public static TaskStatus Create(TaskState value) => new TaskStatus(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
