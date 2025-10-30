using TaskManager.Domain.ValueObjects.Tasks;

namespace TaskManager.Domain.DomainEvents.Tasks;

public sealed record TaskCreatedDomainEvent(TaskId TaskId) : IDomainEvent;
