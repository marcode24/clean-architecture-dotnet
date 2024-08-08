using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Specifications;

public class SpecificationEvaluator<TEntity, TEntityId>
  where TEntity : Entity<TEntityId>
  where TEntityId : class
{
  public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TEntityId> specification)
  {
    if (specification.Criteria is not null)
      inputQuery = inputQuery.Where(specification.Criteria);

    if (specification.OrderBy is not null)
      inputQuery = inputQuery.OrderBy(specification.OrderBy);

    if (specification.OrderByDescending is not null)
      inputQuery = inputQuery.OrderByDescending(specification.OrderByDescending);

    if (specification.IsPagingEnabled)
      inputQuery = inputQuery.Skip(specification.Skip).Take(specification.Take);

    inputQuery = specification.Includes!.Aggregate(inputQuery, (current, include) => current.Include(include))
      .AsSplitQuery()
      .AsNoTracking();

    return inputQuery;
  }
}