using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{
  private readonly IUserRepository _userRepository;
  private readonly IVehiculosRepository _vehiculoRepository;
  private readonly IAlquilerRepository _alquilerRepository;
  private readonly PrecioService _precioService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IDateTimeProvider _dateTimeProvider;

  public ReservarAlquilerCommandHandler(
    IUserRepository userRepository,
    IVehiculosRepository vehiculoRepository,
    IAlquilerRepository alquilerRepository,
    PrecioService precioService,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider
  )
  {
    _userRepository = userRepository;
    _vehiculoRepository = vehiculoRepository;
    _alquilerRepository = alquilerRepository;
    _precioService = precioService;
    _unitOfWork = unitOfWork;
    _dateTimeProvider = dateTimeProvider;
  }

  public async Task<Result<Guid>> Handle(ReservarAlquilerCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UsuarioId.Value);
      var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
      if (user is null)
        return Result.Failure<Guid>(UsersErrors.NotFound);

      var vehiculoId = new VehiculoId(request.VehiculoId.Value);
      var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId!, cancellationToken);
      if (vehiculo is null)
        return Result.Failure<Guid>(VehiculoErrors.NotFound);

      var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);
      if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        return Result.Failure<Guid>(AlquilerErrors.Overlap);

      var alquiler = Alquiler.Reservar(vehiculo, user.Id!, duracion, _dateTimeProvider.currentTime, _precioService);
      _alquilerRepository.Add(alquiler);

      await _unitOfWork.SaveChangesAsync(cancellationToken);

      return alquiler.Id!.Value;
    }
    catch (ConcurrencyException)
    {
      return Result.Failure<Guid>(AlquilerErrors.Overlap);
    }
  }
}