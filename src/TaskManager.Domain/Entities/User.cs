using TaskManager.Domain.Primitives;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public DisplayName DisplayName { get; private set; }
    public Email EMail { get; private set; }

    public Role Role { get; private set; }

    public DateTime JoinedAt { get; private set; }

    public bool Active { get; private set; }

    private User(UserId id, DisplayName displayName, Email email, Role role) : base(id)
    {
        DisplayName = displayName;
        EMail = email;
        Role = role;
        JoinedAt = DateTime.UtcNow;
        Active = true;
    }

    public static Result<User> Create(string displayName, string email, Role role)
    {
        var displayNameResult = DisplayName.Create(displayName);
        if (displayNameResult.IsFailure)
        {
            return displayNameResult.Error!;
        }

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            return emailResult.Error!;
        }
        return new User(UserId.Create(), displayNameResult.Value!, emailResult.Value!, role);
    }

    public Result Activate()
    {
        Active = true;

        return Result.Success();
    }

    public Result Deactivate()
    {
        Active = false;

        return Result.Success();
    }

    public Result UpdateDisplayName(string newDisplayName)
    {
        var displayNameResult = DisplayName.Create(newDisplayName);
        if (displayNameResult.IsFailure)
        {
            return displayNameResult.Error!;
        }

        DisplayName = displayNameResult.Value!;

        return Result.Success();
    }

    public Result UpdateEmail(string newEmail)
    {
        var emailResult = Email.Create(newEmail);
        if (emailResult.IsFailure)
        {
            return emailResult.Error!;
        }

        EMail = emailResult.Value!;

        return Result.Success();
    }

    public Result UpdateRole(Role newRole)
    {
        Role = newRole;

        return Result.Success();
    }
}
