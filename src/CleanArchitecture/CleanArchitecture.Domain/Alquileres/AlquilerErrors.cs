using System.Net.NetworkInformation;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

public static class AlquilerErrors
{
  public static Error NotFound = new(
    "alquiler_not_found",
    "El alquiler no existe."
  );
  public static Error Overlap = new(
    "alquiler_overlap",
    "El alquiler se superpone con otro."
  );
  public static Error NotReserved = new(
    "alquiler_not_reserved",
    "El alquiler no está reservado."
  );
  public static Error NotConfirmed = new(
    "alquiler_not_confirmed",
    "El alquiler no está confirmado."
  );
  public static Error AlreadyStarted = new(
    "alquiler_already_started",
    "El alquiler ya ha comenzado."
  );
}