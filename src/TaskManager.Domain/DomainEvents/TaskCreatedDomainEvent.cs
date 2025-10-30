using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record TaskCreatedDomainEvent(TaskId TaskId) : IDomainEvent;
