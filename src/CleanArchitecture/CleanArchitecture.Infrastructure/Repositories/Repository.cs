using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
  protected readonly ApplicationDbContext _dbContext;
  protected Repository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<T>()
      .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
      ?? throw new Exception("Entity not found.");
  }

  public void Add(T entity)
  {
    _dbContext.Set<T>().Add(entity);
  }
}