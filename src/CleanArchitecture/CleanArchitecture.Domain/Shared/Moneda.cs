namespace CleanArchitecture.Domain.Shared;

public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
{
  public static Moneda operator +(Moneda a, Moneda b)
  {
    if (a.TipoMoneda != b.TipoMoneda) throw new InvalidOperationException("No se pueden sumar montos de diferentes monedas");
    return new Moneda(a.Monto + b.Monto, a.TipoMoneda);
  }

  public static Moneda Zero() => new(0, TipoMoneda.None);
  public static Moneda Zero(TipoMoneda tipoMoneda) => new(0, tipoMoneda);
  public bool IsZero() => this == Zero(TipoMoneda);
}