using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public class TaskId : ValueObject
{
    public string Value { get; }

    private TaskId(string value)
    {
        Value = value;
    }

    public static TaskId Create() => new TaskId(DateTime.Now.ToShortDateString());// TODO: Format: TASK-YYYYMMDD-XXXX

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
