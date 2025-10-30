using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors;

public static class ProjectErrors
{
    public static Error NameEmpty => new Error("Project.Name.Empty", "プロジェクト名は空には出来ません。");
    public static Error NameTooLong => new Error("Project.Name.TooLong", "プロジェクト名は100文字以内にしてください。");

    public static Error StatusInvalidTransition = > new Error("Project.Status.InvalidTransition", "プロジェクトの状態遷移が無効です。");
}
