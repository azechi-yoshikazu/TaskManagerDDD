
namespace TaskManager.Domain.ValueObjects;

public class DueDate : DateTimeVO
{
    private DueDate(DateTime value) : base(value)
    {
    }

    public static DueDate Create(DateTime value) => new DueDate(value);
}
