using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
  public static readonly Role Cliente = new(1, "Cliente");
  public static readonly Role Administrador = new(2, "Administrador");
  public Role(int id, string name) : base(id, name) { }
  public ICollection<Permission>? Permissions { get; set; }
}