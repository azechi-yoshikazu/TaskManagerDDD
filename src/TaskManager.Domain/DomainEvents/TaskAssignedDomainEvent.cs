using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record TaskAssignedDomainEvent(TaskId TaskId, UserId? AssignedUserId) : IDomainEvent;