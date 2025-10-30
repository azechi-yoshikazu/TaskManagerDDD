using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.DomainEvents;

public sealed record UserRegisteredDomainEvent(UserId UserId) : IDomainEvent;
