using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public abstract class DateTimeVO : ValueObject
{
    public DateTime Value { get; }

    protected DateTimeVO(DateTime value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
