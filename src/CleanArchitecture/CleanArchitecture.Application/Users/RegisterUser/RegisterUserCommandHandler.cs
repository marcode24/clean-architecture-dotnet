using System.Linq.Expressions;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWord;
  public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWord)
  {
    _userRepository = userRepository;
    _unitOfWord = unitOfWord;
  }

  public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    var email = new Email(request.Email);
    var userExists = await _userRepository.IsUserExists(email, cancellationToken);

    if (userExists)
      return Result.Failure<Guid>(UsersErrors.AlreadyExists);

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
    var user = User.Create(
      new Nombre(request.Nombre),
      new Apellido(request.Apellido),
      new Email(request.Email),
      new PasswordHash(passwordHash)
    );

    _userRepository.Add(user);

    await _unitOfWord.SaveChangesAsync(cancellationToken);

    return Result.Success(user.Id!.Value);
  }
}