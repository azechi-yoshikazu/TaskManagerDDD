using TaskManager.Domain.ValueObjects.Tasks;

namespace TaskManager.Domain.DomainEvents.Tasks;

public sealed record TaskCompletedDomainEvent(TaskId TaskId) : IDomainEvent;