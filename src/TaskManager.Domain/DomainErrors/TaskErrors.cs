using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors
{
    public static class TaskErrors
    {
        public static Error TitleEmpty => new Error("Task.Title.Empty", "The task title cannot be empty.");
        public static Error TitleTooLong => new Error("Task.Title.TooLong", "The task title exceeds the maximum allowed length.");
    }
}
