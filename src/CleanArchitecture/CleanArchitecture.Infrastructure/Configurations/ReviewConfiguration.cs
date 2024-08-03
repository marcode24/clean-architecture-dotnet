using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
  public void Configure(EntityTypeBuilder<Review> builder)
  {
    builder.ToTable("reviews");
    builder.HasKey(r => r.Id);

    builder.Property(r => r.Rating)
      .HasConversion(r => r.Value, v => Rating.Create(v).Value);

    builder.Property(r => r.Comentario)
      .HasMaxLength(200)
      .HasConversion(c => c.Value, v => new Comentario(v));

    builder.HasOne<Vehiculo>()
      .WithMany()
      .HasForeignKey(r => r.VehiculoId);

    builder.HasOne<Alquiler>()
      .WithMany()
      .HasForeignKey(r => r.AlquilerId);

    builder.HasOne<User>()
      .WithMany()
      .HasForeignKey(r => r.UserId);
  }
}