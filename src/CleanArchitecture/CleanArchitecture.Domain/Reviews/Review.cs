using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity
{
  private Review(
    Guid id,
    Guid vehiculoId,
    Guid alquilerId,
    Guid userId,
    int rating,
    string comentario,
    DateTime fechaCreacion
  ) : base(id)
  {
    VehiculoId = vehiculoId;
    AlquilerId = alquilerId;
    UserId = userId;
    Rating = rating;
    Comentario = comentario;
    FechaCreacion = fechaCreacion;
  }
  public Guid VehiculoId { get; private set; }
  public Guid AlquilerId { get; private set; }
  public Guid UserId { get; private set; }
  public int Rating { get; private set; }
  public string Comentario { get; private set; } = string.Empty;
  public DateTime FechaCreacion { get; private set; }

  public static Result<Review> Create(
    Alquiler alquiler,
    int rating,
    string comentario,
    DateTime fechaCreacion
  )
  {
    if (alquiler.Status != AlquilerStatus.Completado)
      return Result.Failure<Review>(ReviewErrors.NotEligible);

    var review = new Review(
      Guid.NewGuid(),
      alquiler.VehiculoId,
      alquiler.Id,
      alquiler.UsuarioId,
      rating,
      comentario,
      fechaCreacion
    );

    review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

    return review;
  }
}