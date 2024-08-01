using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public record AlquilerCanceladoDomainEvent(Guid AlquilerId) : IDomainEvent;