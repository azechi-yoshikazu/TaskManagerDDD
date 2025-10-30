using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record ProjectCompletedDomainEvent(ProjectId ProjectId) : IDomainEvent;
