using TaskManager.Domain.ValueObjects.Users;

namespace TaskManager.Domain.DomainEvents.Users;

public sealed record UserDeactivatedDomainEvent(UserId UserId) : IDomainEvent;
