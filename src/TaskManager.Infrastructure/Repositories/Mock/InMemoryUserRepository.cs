using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Infrastructure.Repositories.Mock;

public sealed class InMemoryUserRepository : IUserRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public User? FindById(UserId userId)
    {
        throw new NotImplementedException();
    }

    public void Remove(User user)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
