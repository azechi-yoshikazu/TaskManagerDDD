using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors;

public static class TaskErrors
{
    public static Error TitleEmpty => new Error("Task.Title.Empty", "タスクタイトルは空には出来ません。");
    public static Error TitleTooLong => new Error("Task.Title.TooLong", "タスクのタイトルは100文字以内にしてください");

    public static Error DescriptionTooLong => new Error("Task.Description.TooLong", "タスクの説明は1000文字以内にしてください。");

    public static Error StatusInvalidTransition => new Error("Task.Status.InvalidTransition", "タスクの状態遷移が無効です。");
}
