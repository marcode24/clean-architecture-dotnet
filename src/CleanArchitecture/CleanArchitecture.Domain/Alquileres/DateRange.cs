namespace CleanArchitecture.Domain.Alquileres;

public sealed record DateRange
{
  private DateRange()
  {

  }

  public DateOnly Inicio { get; init; }
  public DateOnly Fin { get; init; }
  public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;
  public static DateRange Create(DateOnly inicio, DateOnly fin)
  {
    if (inicio > fin) throw new ApplicationException("La fecha de inicio no puede ser mayor a la fecha de fin");

    return new DateRange
    {
      Inicio = inicio,
      Fin = fin
    };
  }
}