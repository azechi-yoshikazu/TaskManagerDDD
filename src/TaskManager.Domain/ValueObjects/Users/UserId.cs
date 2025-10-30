using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Users;

public sealed class UserId : ValueObject
{
    // TODO: string and format USER-XXXXXX
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create() => new UserId(Guid.NewGuid());

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
