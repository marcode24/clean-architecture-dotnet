using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId> where TEntity : Entity<TEntityId> where TEntityId : class
{
  protected readonly ApplicationDbContext _dbContext;
  protected Repository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TEntity>()
      .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
  }

  public void Add(TEntity entity)
  {
    _dbContext.Set<TEntity>().Add(entity);
  }

  public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TEntityId> specification)
  {
    return SpecificationEvaluator<TEntity, TEntityId>
      .GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
  }

  public async Task<IReadOnlyList<TEntity>> GetALllWithSpec(
    ISpecification<TEntity, TEntityId> specification,
    CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).ToListAsync(cancellationToken);
  }

  public async Task<int> CountAsync(
      ISpecification<TEntity, TEntityId> specification,
      CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).CountAsync(cancellationToken);
  }
}