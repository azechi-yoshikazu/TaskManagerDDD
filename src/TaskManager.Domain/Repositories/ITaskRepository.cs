using TaskManager.Domain.ValueObjects.Tasks;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.Repositories;

public interface ITaskRepository
{
    void Add(Entities.Task task);
    void Update(Entities.Task task);
    void Remove(Entities.Task task);
    Entities.Task? FindById(TaskId taskId);
    IEnumerable<Entities.Task> FindByUserId(UserId userId);
}
