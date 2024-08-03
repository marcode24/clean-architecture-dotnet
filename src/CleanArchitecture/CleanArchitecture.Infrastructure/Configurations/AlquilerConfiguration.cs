using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class AlquilerConfiguration : IEntityTypeConfiguration<Alquiler>
{
  public void Configure(EntityTypeBuilder<Alquiler> builder)
  {
    builder.ToTable("alquileres");
    builder.HasKey(a => a.Id);

    builder.OwnsOne(a => a.PrecioPorPeriodo, pb =>
    {
      pb.Property(m => m.TipoMoneda)
        .HasConversion(tm => tm.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.OwnsOne(a => a.Mantenimiento, pb =>
    {
      pb.Property(m => m.TipoMoneda)
        .HasConversion(tm => tm.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.OwnsOne(a => a.Accesorios, pb =>
    {
      pb.Property(m => m.TipoMoneda)
        .HasConversion(tm => tm.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.OwnsOne(a => a.PrecioTotal, pb =>
    {
      pb.Property(m => m.TipoMoneda)
        .HasConversion(tm => tm.Codigo, c => TipoMoneda.FromCodigo(c));
    });

    builder.OwnsOne(a => a.Duracion);

    builder.HasOne<Vehiculo>()
      .WithMany()
      .HasForeignKey(a => a.VehiculoId);

    builder.HasOne<User>()
      .WithMany()
      .HasForeignKey(a => a.UsuarioId);
  }
}