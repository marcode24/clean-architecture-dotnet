namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;
public sealed class DireccionResponse
{
  public string Pais { get; init; } = string.Empty;
  public string Departamento { get; init; } = string.Empty;
  public string Provincia { get; init; } = string.Empty;
  public string Calle { get; init; } = string.Empty;
}