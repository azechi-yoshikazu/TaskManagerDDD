using TaskManager.Domain.ValueObjects.Projects;

namespace TaskManager.Domain.DomainEvents.Projects;

public sealed record ProjectCreatedDomainEvent(ProjectId ProjectId) : IDomainEvent;
