using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public record AlquilerRechazadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;