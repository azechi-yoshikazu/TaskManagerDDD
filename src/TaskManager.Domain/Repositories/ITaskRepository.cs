using TaskManager.Domain.ValueObjects.Tasks;

namespace TaskManager.Domain.Repositories;

public interface ITaskRepository
{
    void Add(Entities.Task task);
    void Update(Entities.Task task);
    void Remove(Entities.Task task);
    Entities.Task? FindById(TaskId taskId);
}
