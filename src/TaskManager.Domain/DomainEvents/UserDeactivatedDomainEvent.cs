using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record UserDeactivatedDomainEvent(UserId UserId) : IDomainEvent;
