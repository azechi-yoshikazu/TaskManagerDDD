namespace TaskManager.Domain.DomainEvents
{
    public interface IDomainEventDispather
    {
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
