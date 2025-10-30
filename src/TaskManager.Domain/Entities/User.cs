using TaskManager.Domain.Primitives;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public DisplayName DisplayName { get; private set; }
    public Email Email { get; private set; }

    public Role Role { get; private set; }

    public DateTime JoinedAt { get; private set; }

    public bool Active { get; private set; }

    private User(UserId id, DisplayName displayName, Email email, Role role) : base(id)
    {
        DisplayName = displayName;
        Email = email;
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
        var user = new User(UserId.Create(), displayNameResult.Value!, emailResult.Value!, role);
        user.RaiseDomainEvent(new DomainEvents.UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public Result Activate()
    {
        Active = true;

        return Result.Success();
    }

    public Result Deactivate()
    {
        Active = false;
        RaiseDomainEvent(new DomainEvents.UserDeactivatedDomainEvent(Id));

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

        Email = emailResult.Value!;

        return Result.Success();
    }

    public Result UpdateRole(Role newRole)
    {
        Role = newRole;

        return Result.Success();
    }
}
