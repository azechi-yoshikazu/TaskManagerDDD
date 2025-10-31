using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors;

public static class ProjectErrors
{
    public static Error NotFound => new Error("Project.NotFound", "プロジェクトが存在しません。");

    public static Error NameEmpty => new Error("Project.Name.Empty", "プロジェクト名は空には出来ません。");
    public static Error NameTooLong => new Error("Project.Name.TooLong", "プロジェクト名は100文字以内にしてください。");

    public static Error StatusInvalidTransition => new Error("Project.Status.InvalidTransition", "プロジェクトの状態遷移が無効です。");

    public static Error MemberAlreadyExists => new Error("Project.Member.AlreadyExists", "メンバーはすでにプロジェクトに参加しています。");
    public static Error MemberNotFound => new Error("Project.Member.NotFound", "メンバーがプロジェクトに存在しません。");

    public static Error TaskAlreadyExists => new Error("Project.Task.AlreadyExists", "タスクはすでにプロジェクトに存在します。");
    public static Error TaskNotFound => new Error("Project.Task.NotFound", "タスクがプロジェクトに存在しません。");
}
