
namespace TaskManager.Domain.ValueObjects
{
    public class CompletedAt : DateTimeVO
    {
        private CompletedAt(DateTime value) : base(value)
        {
        }

        public static CompletedAt Create() => new CompletedAt(DateTime.Now);
    }
}
