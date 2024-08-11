using System.Text;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Users.GetUsersDapperPagination;

internal sealed class GetUsersDapperPaginationQueryHandler : IQueryHandler<GetUsersDapperPaginationQuery, PagedDapperResults<UserPaginationData>>
{
  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public GetUsersDapperPaginationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  public async Task<Result<PagedDapperResults<UserPaginationData>>> Handle(GetUsersDapperPaginationQuery request, CancellationToken cancellationToken)
  {
    using var connection = _sqlConnectionFactory.CreateConnection();
    var builder = new StringBuilder("""
      SELECT
        usr.email as email,
        rl.name as role,
        p.name as permiso
      FROM users usr
        LEFT JOIN users_roles usr_rol
          ON usr.id = usr_rol.user_id
        LEFT JOIN roles rl
          ON rl.id = usr_rol.role_id
        LEFT JOIN roles_permissions rp
          ON rl.id = rp.role_id
        LEFT JOIN permissions p
          ON p.id = rp.permission_id
    """);

    var search = string.Empty;
    var whereStatement = string.Empty;
    if (!string.IsNullOrEmpty(request.Search))
    {
      search = "%" + EncodeForLike(request.Search) + "%";
      whereStatement = $" WHERE rl.name LIKE @Search ";
      builder.AppendLine(whereStatement);
    }

    var orderBy = request.OrderBy;
    if (!string.IsNullOrEmpty(orderBy))
    {
      var orderStatement = string.Empty;
      var orderAsc = request.OrderAsc ? "ASC" : "DESC";
      orderStatement = $"ORDER BY {orderBy} {orderAsc}";
      orderStatement = orderBy switch
      {
        "email" => $" ORDER BY usr.email {orderAsc}",
        "role" => $" ORDER BY rl.name {orderAsc}",
        _ => $" ORDER BY p.name {orderAsc}",
      };
      builder.AppendLine(orderStatement);
    }

    builder.AppendLine("LIMIT @PageSize OFFSET @Offset;");

    builder.AppendLine("""
      SELECT
        COUNT(*)
      FROM users usr
        LEFT JOIN users_roles usr_rol
          ON usr.id = usr_rol.user_id
        LEFT JOIN roles rl
          ON rl.id = usr_rol.role_id
        LEFT JOIN roles_permissions rp
          ON rl.id = rp.role_id
        LEFT JOIN permissions p
          ON p.id = rp.permission_id
    """);

    builder.AppendLine(whereStatement);
    builder.AppendLine(";");

    var sql = builder.ToString();
    var offset = request.PageSize * (request.PageNumber - 1);
    using var multi = await connection.QueryMultipleAsync(sql, new
    {
      PageSize = request.PageSize,
      Offset = offset,
      Search = search
    });

    var items = await multi.ReadAsync<UserPaginationData>().ConfigureAwait(false);
    var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

    var result = new PagedDapperResults<UserPaginationData>(totalItems, request.PageNumber, request.PageSize)
    {
      Items = items
    };

    return result;
  }

  private static string EncodeForLike(string value)
  {
    return value.Replace("[", "[]]").Replace("%", "[%]");
  }
}