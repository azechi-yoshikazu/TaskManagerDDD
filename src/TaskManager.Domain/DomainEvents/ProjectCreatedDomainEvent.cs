using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record ProjectCreatedDomainEvent(ProjectId ProjectId) : IDomainEvent;
