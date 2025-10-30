using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Repositories;

public interface IUserRepository
{
    void Add(Entities.User user);
    void Update(Entities.User user);
    void Remove(Entities.User user);

    Entities.User? FindById(UserId userId);
}
