using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Users;

public sealed class DisplayName : ValueObject
{
    public string Value { get; }

    private DisplayName(string value)
    {
        Value = value;
    }

    public static Result<DisplayName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return UserErrors.DisplayNameIsRequired;
        }

        return new DisplayName(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
