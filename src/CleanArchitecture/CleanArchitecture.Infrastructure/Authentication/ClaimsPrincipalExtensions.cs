using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanArchitecture.Domain.Users;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
  public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
  {
    return claimsPrincipal.FindFirstValue(ClaimTypes.Email)
      ?? throw new ApplicationException("User email not found in claims");
  }

  public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
  {
    var userIdOV = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

    return Guid.TryParse(userIdOV, out var userId)
      ? userId
      : throw new ApplicationException("User id not found in claims");
  }
}