using CleanArchitecture.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.ToTable("permissions");
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id)
      .HasConversion(p => p!.Value, v => new PermissionId(v));

    builder.Property(x => x.Nombre)
      .HasConversion(p => p!.Value, v => new Nombre(v));

    IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
      .Select(p => new Permission(new PermissionId((int)p), new Nombre(p.ToString())));

    builder.HasData(permissions);
  }
}