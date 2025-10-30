using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record ProjectArchivedDomainEvent(ProjectId ProjectId) : IDomainEvent;