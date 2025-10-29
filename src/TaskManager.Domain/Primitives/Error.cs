namespace TaskManager.Domain.Primitives
{
    public sealed record Error(string Code, string? Message = null)
    {
        public static Error None => new Error(string.Empty);
    }
}
