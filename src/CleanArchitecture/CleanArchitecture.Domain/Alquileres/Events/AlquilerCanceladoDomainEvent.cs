using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public record AlquilerCanceladoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;