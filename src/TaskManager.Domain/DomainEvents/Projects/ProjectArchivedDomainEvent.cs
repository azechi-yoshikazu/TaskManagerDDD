using TaskManager.Domain.ValueObjects.Projects;

namespace TaskManager.Domain.DomainEvents.Projects;

public sealed record ProjectArchivedDomainEvent(ProjectId ProjectId) : IDomainEvent;