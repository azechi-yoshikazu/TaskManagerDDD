using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects.Tasks;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Infrastructure.Repositories.Mock;

internal class InMemoryTaskRepository : ITaskRepository
{
    private Dictionary<TaskId, Domain.Entities.Task> _tasks = new();

    public void Add(Domain.Entities.Task task)
    {
        if (!_tasks.ContainsKey(task.Id))
        {
            _tasks[task.Id] = task;
        }
    }

    public Domain.Entities.Task? FindById(TaskId taskId)
    {
        if (_tasks.TryGetValue(taskId, out var task))
        {
            return task;
        }

        return null;
    }

    public IEnumerable<Domain.Entities.Task> FindByUserId(UserId userId)
    {
        return _tasks.Values.Where(x => x.AssignedUserId == userId);
    }

    public void Remove(Domain.Entities.Task task)
    {
        if (_tasks.ContainsKey(task.Id))
        {
            _tasks.Remove(task.Id);
        }
    }

    public void Update(Domain.Entities.Task task)
    {
        if (_tasks.ContainsKey(task.Id))
        {
            _tasks[task.Id] = task;
        }
    }
}
