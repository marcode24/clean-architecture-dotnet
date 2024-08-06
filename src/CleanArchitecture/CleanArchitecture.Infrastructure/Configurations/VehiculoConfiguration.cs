using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
  public void Configure(EntityTypeBuilder<Vehiculo> builder)
  {
    builder.ToTable("vehiculos");
    builder.HasKey(v => v.Id);

    builder.Property(v => v.Id)
      .HasConversion(id => id!.Value, value => new VehiculoId(value));

    builder.OwnsOne(v => v.Direccion);

    builder.Property(v => v.Modelo)
      .HasMaxLength(200)
      .HasConversion(m => m!.Value, v => new Modelo(v));

    builder.Property(v => v.Vin)
      .HasMaxLength(500)
      .HasConversion(v => v!.Value, v => new Vin(v));

    builder.OwnsOne(v => v.Precio, pb =>
    {
      pb.Property(p => p.TipoMoneda)
        .HasConversion(tp => tp.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.OwnsOne(v => v.Mantenimiento, pb =>
    {
      pb.Property(p => p.TipoMoneda)
        .HasConversion(tp => tp.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.Property<uint>("Version").IsRowVersion();
  }
}