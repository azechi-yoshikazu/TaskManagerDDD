using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record TaskCompletedDomainEvent(TaskId TaskId) : IDomainEvent;