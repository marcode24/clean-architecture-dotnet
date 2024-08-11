using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository, IPaginationRepository
{
  public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

  public async Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<User>()
      .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
  }

  public async Task<bool> IsUserExists(Domain.Users.Email email, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<User>()
      .AnyAsync(u => u.Email == email, cancellationToken);
  }

  public override void Add(User user)
  {
    foreach (var role in user.Roles!)
    {
      _dbContext.Attach(role);
    }
    _dbContext.Add(user);
  }
}