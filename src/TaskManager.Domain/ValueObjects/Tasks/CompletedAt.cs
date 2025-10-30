namespace TaskManager.Domain.ValueObjects.Tasks
{
    public class CompletedAt : DateTimeVO
    {
        private CompletedAt(DateTime value) : base(value)
        {
        }

        public static CompletedAt Create() => new CompletedAt(DateTime.UtcNow);
    }
}
