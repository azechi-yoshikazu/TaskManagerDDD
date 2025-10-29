namespace TaskManager.Domain.Primitives;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return NotEqualOperator(left, right);
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null or not ValueObject)
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(0, (hash, component) =>
            {
                var componentHash = component?.GetHashCode() ?? 0;
                return HashCode.Combine(hash, componentHash);
            });
    }
}
