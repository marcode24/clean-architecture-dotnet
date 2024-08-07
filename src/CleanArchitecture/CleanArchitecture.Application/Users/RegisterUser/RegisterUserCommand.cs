using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Nombre, string Apellido, string Password) : ICommand<Guid>;