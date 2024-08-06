using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
  public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

  public async Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<User>()
      .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
  }
}