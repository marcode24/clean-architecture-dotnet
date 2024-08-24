using CleanArchitecture.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserContext(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string Email => _httpContextAccessor.HttpContext?.User.GetUserEmail()
    ?? throw new ApplicationException("User email not found in context");

  public Guid UserId => _httpContextAccessor.HttpContext?.User.GetUserId()
    ?? throw new ApplicationException("User not found in context");
}