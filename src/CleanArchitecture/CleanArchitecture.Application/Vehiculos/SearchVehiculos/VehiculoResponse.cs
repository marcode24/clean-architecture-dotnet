namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public sealed class VehiculoResponse
{
  public Guid Id { get; init; }
  public string Modelo { get; init; } = string.Empty;
  public string Vin { get; init; } = string.Empty;
  public decimal Precio { get; init; }
  public string TipoMoneda { get; init; } = string.Empty;
  public DireccionResponse Direccion { get; set; } = new();
}