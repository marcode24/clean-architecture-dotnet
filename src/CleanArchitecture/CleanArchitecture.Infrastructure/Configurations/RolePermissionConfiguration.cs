using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
  public void Configure(EntityTypeBuilder<RolePermission> builder)
  {
    builder.ToTable("roles_permissions");
    builder.HasKey(x => new { x.RoleId, x.PermissionId });

    builder.Property(x => x.PermissionId)
      .HasConversion(p => p!.Value, p => new PermissionId(p));

    builder.HasData(
      Create(Role.Cliente, PermissionEnum.ReadUser),
      Create(Role.Administrador, PermissionEnum.WriteUser),
      Create(Role.Administrador, PermissionEnum.UpdateUser),
      Create(Role.Administrador, PermissionEnum.ReadUser)
    );
  }

  private static RolePermission Create(Role role, PermissionEnum permission)
  {
    return new RolePermission
    {
      RoleId = role.Id,
      PermissionId = new PermissionId((int)permission)
    };
  }
}