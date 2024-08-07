namespace CleanArchitecture.Application.Users.RegisterUser;

public record RegisterUserRequest(string Email, string Nombre, string Apellido, string Password);