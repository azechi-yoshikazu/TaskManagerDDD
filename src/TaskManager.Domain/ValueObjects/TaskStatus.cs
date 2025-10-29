using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public enum TaskState
{
    Pending,
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

    public static Result<TaskStatus> Create(TaskState value)
    {
        return new TaskStatus(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
