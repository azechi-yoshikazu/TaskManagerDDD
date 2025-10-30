using System.Text.RegularExpressions;
using TaskManager.Domain.DomainErrors;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return UserErrors.EmailIsRequired;
        }

        if(!IsValidFormat(value))
        {
            return UserErrors.EmailInvalidFormat;
        }

        return new Email(value);
    }

    private static bool IsValidFormat(string email)
    {
        Regex emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return emailRegex.IsMatch(email);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
