namespace CleanArchitecture.Domain.Vehiculos;

public record TipoMoneda
{
  public static readonly TipoMoneda None = new(string.Empty);
  public static readonly TipoMoneda Usd = new("USD");
  public static readonly TipoMoneda Eur = new("EUR");
  private TipoMoneda(string codigo) => Codigo = codigo;
  public string Codigo { get; init; } = string.Empty;
  public static readonly IReadOnlyCollection<TipoMoneda> All = [Usd, Eur];
  public static TipoMoneda FromCodigo(string codigo) => All.FirstOrDefault(x => x.Codigo == codigo)
    ?? throw new InvalidOperationException($"Tipo de moneda no soportado: {codigo}");
}