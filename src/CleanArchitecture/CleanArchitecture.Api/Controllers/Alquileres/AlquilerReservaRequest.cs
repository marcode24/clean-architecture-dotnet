using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Api.Controllers.Alquileres;

public sealed record AlquilerReservaRequest(VehiculoId VehiculoId, UserId UserId, DateOnly StartDate, DateOnly EndDate);