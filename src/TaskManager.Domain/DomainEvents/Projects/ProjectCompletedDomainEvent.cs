using TaskManager.Domain.ValueObjects.Projects;

namespace TaskManager.Domain.DomainEvents.Projects;

public sealed record ProjectCompletedDomainEvent(ProjectId ProjectId) : IDomainEvent;
