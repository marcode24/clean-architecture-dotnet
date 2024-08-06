using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events;

public record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;