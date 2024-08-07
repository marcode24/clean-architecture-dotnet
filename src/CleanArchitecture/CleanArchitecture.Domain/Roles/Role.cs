using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
  public static readonly Role Cliente = new(1, "Cliente");
  public static readonly Role Administrador = new(1, "Administrador");
  public Role(int id, string name) : base(id, name) { }
}