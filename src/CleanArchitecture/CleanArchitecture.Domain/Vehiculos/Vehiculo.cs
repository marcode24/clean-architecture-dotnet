using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
  public Vehiculo(
      Guid id,
      Modelo modelo,
      Vin vin,
      Direccion direccion,
      Moneda moneda,
      Moneda mantenimiento,
      DateTime fechaUltimoAlquiler,
      List<Accesorio> accesorios
      ) : base(id)
  {
    Modelo = modelo;
    Vin = vin;
    Direccion = direccion;
    Precio = moneda;
    Mantenimiento = mantenimiento;
    FechaUltimoAlquiler = fechaUltimoAlquiler;
    Accesorios = accesorios;
  }
  public Modelo? Modelo { get; private set; }
  public Vin? Vin { get; private set; }
  public Direccion? Direccion { get; private set; }
  public Moneda? Precio { get; private set; }
  public Moneda? Mantenimiento { get; private set; }
  public DateTime FechaUltimoAlquiler { get; internal set; }
  public List<Accesorio> Accesorios { get; private set; } = [];
}