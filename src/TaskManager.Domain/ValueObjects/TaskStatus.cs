using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.ValueObjects;

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
}