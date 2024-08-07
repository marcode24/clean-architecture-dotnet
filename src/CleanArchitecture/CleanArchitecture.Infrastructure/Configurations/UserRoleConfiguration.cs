using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("user_roles");
    builder.HasKey(x => new { x.RoleId, x.UserId });

    builder.Property(u => u.UserId)
      .HasConversion(id => id!.Value, value => new UserId(value));
  }
}