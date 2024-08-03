namespace CleanArchitecture.Application.Alquileres.GetAlquiler;

public sealed class AlquilerResponse
{
  public Guid Id { get; set; }
  public Guid UserId { get; init; }
  public Guid VehiculoId { get; init; }
  public int Status { get; init; }
  public decimal PrecioAlquiler { get; init; }
  public string TipoMonedaAlquiler { get; init; } = string.Empty;
  public decimal PrecioMantenimiento { get; init; }
  public string TipoMonedaMantenimiento { get; init; } = string.Empty;
  public decimal AccesorioPrecio { get; init; }
  public string TipoMonedaAccesorio { get; init; } = string.Empty;
  public decimal PrecioTotal { get; init; }
  public string PrecioTotalTipoMoneda { get; init; } = string.Empty;
  public DateOnly DuracionInicio { get; init; }
  public DateOnly DuracionFin { get; init; }
  public DateTime FechaCreacion { get; init; }
}