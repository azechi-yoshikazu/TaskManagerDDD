using TaskManager.Domain.Entities;
using TaskManager.Domain.Primitives;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Application.ApplicationServices;

public sealed class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Result<User> RegisterUser(string displayName, string email, Role role)
    {
        var createResult = User.Create(displayName, email, role);
        if (createResult.IsFailure)
        {
            return createResult.Error!;
        }
        var user = createResult.Value!;

        _userRepository.Add(user);

        return user;
    }

    public Result<User> UpdateUserProfile(UserId userId, string? newDisplayName, string? newEmail)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return Domain.DomainErrors.UserErrors.NotFound;
        }

        if(newDisplayName is not null)
        {
            var displayNameResult = user.UpdateDisplayName(newDisplayName);
            if (displayNameResult.IsFailure)
            {
                return displayNameResult.Error!;
            }
        }

        if (newEmail is not null)
        {
            var emailResult = user.UpdateEmail(newEmail);
            if (emailResult.IsFailure)
            {
                return emailResult.Error!;
            }
        }

        return user;
    }

    public Result<User> ChangeUserRole(UserId userId, Role newRole)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return Domain.DomainErrors.UserErrors.NotFound;
        }
        
        var roleResult = user.UpdateRole(newRole);
        if (roleResult.IsFailure)
        {
            return roleResult.Error!;
        }

        _userRepository.Update(user);

        return user;
    }

    public Result<User> ActivateUser(UserId userId)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return Domain.DomainErrors.UserErrors.NotFound;
        }

        var activateResult = user.Activate();
        if (activateResult.IsFailure)
        {
            return activateResult.Error!;
        }
     
        _userRepository.Update(user);
        return user;
    }

    public Result<User> DeactivateUser(UserId userId)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return Domain.DomainErrors.UserErrors.NotFound;
        }

        var deactivateResult = user.Deactivate();
        if (deactivateResult.IsFailure)
        {
            return deactivateResult.Error!;
        }

        _userRepository.Update(user);
        return user;
    }

    public Result DeleteUser(UserId userId)
    {
        var user = _userRepository.FindById(userId);
        if (user is null)
        {
            return Domain.DomainErrors.UserErrors.NotFound;
        }
        
        _userRepository.Remove(user);

        return Result.Success();
    }
}
