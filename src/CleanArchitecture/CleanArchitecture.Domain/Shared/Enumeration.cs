using System.Reflection;

namespace CleanArchitecture.Domain.Shared;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
{
  private static readonly Dictionary<int, TEnum> _enumerations = CreateEnumerations();
  public int Id { get; protected init; }
  public string Name { get; protected init; } = string.Empty;
  public Enumeration(int id, string name)
  {
    Id = id;
    Name = name;
  }
  public static TEnum FromValue(int id)
  {
    return _enumerations.TryGetValue(id, out TEnum? enumeration)
      ? enumeration
      : default!;
  }
  public static TEnum? FromName(string name) => _enumerations.Values.SingleOrDefault(e => e.Name == name);
  public static List<TEnum> GetValues() => _enumerations.Values.ToList();
  public bool Equals(Enumeration<TEnum>? other)
  {
    if (other is null)
      return false;

    return GetType() == other.GetType() && Id == other.Id;
  }
  public override bool Equals(object? obj) => obj is Enumeration<TEnum> other && Equals(other);
  public override int GetHashCode() => Id.GetHashCode();
  public override string ToString() => Name;
  public static Dictionary<int, TEnum> CreateEnumerations()
  {
    var enumerationType = typeof(TEnum);
    var fieldsForType = enumerationType.GetFields(
      BindingFlags.Public |
      BindingFlags.Static |
      BindingFlags.FlattenHierarchy
    ).Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
    .Select(fieldInfo => (TEnum)fieldInfo.GetValue(null)!);

    return fieldsForType.ToDictionary(e => e.Id);
  }
}