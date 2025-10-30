using TaskManager.Domain.ValueObjects.Tasks;
using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.DomainEvents.Tasks;

public sealed record TaskAssignedDomainEvent(TaskId TaskId, UserId? AssignedUserId) : IDomainEvent;