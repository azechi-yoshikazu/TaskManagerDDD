using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Infrastructure.Repositories.Mock;

public sealed class InMemoryUserRepository : IUserRepository
{
    private Dictionary<UserId, User> _users = new();

    public void Add(User user)
    {
        if(!_users.ContainsKey(user.Id))
        {
            _users.Add(user.Id, user);
        }
    }

    public User? FindById(UserId userId)
    {
        if (_users.TryGetValue(userId, out var user))
        {
            return user;
        }

        return null;
    }

    public void Remove(User user)
    {
        if(_users.ContainsKey(user.Id))
        {
            _users.Remove(user.Id);
        }
    }

    public void Update(User user)
    {
        if(_users.ContainsKey(user.Id))
        {
            _users[user.Id] = user;
        }
    }
}
