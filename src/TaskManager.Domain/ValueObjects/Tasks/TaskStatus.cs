using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects.Tasks;

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
}