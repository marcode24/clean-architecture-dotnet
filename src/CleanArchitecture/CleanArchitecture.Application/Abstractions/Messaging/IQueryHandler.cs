using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<IQuery, TResponse> : IRequestHandler<IQuery, Result<TResponse>>
  where IQuery : IQuery<TResponse>
{
}