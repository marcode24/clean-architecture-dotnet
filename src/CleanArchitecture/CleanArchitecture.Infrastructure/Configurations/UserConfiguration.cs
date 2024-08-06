using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");
    builder.HasKey(u => u.Id);

    builder.Property(u => u.Id)
      .HasConversion(id => id!.Value, value => new UserId(value));

    builder.Property(p => p.Nombre)
      .HasMaxLength(200)
      .HasConversion(n => n!.Value, v => new Nombre(v));

    builder.Property(p => p.Apellido)
      .HasMaxLength(200)
      .HasConversion(a => a!.Value, v => new Apellido(v));

    builder.Property(p => p.Email)
      .HasMaxLength(200)
      .HasConversion(e => e!.Value, v => new Domain.Users.Email(v));

    builder.HasIndex(p => p.Email).IsUnique();
  }
}