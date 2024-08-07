
using FluentValidation;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
  public RegisterUserCommandValidator()
  {
    RuleFor(c => c.Nombre)
      .NotEmpty()
      .WithMessage("El nombre es requerido.");

    RuleFor(c => c.Apellido)
      .NotEmpty()
      .WithMessage("El apellido es requerido.");

    RuleFor(c => c.Email)
      .NotEmpty()
      .WithMessage("El email es requerido.")
      .EmailAddress()
      .WithMessage("El email no es válido.");

    RuleFor(c => c.Password)
      .NotEmpty()
      .WithMessage("La contraseña es requerida.")
      .MinimumLength(5)
      .WithMessage("La contraseña debe tener al menos 5 caracteres.");
  }
}