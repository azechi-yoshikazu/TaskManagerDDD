using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors
{
    public static class TaskErrors
    {
        public static Error TitleEmpty => new Error("Task.Title.Empty", "タスクタイトルは空には出来ません。");
        public static Error TitleTooLong => new Error("Task.Title.TooLong", "タスクタイトルが長すぎます");
    }
}
