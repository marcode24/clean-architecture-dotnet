namespace CleanArchitecture.Application.Abstractions.Authentication;

public interface IUserContext
{
  string Email { get; }
  Guid UserId { get; }
}