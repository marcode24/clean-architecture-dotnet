using CleanArchitecture.Application.Users.GetUsersDapperPagination;
using CleanArchitecture.Application.Users.GetUsersPagination;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
  private readonly ISender _sender;
  public UsersController(ISender sender)
  {
    _sender = sender;
  }

  [AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
  {
    var command = new LoginCommand(request.Email, request.Password);
    var result = await _sender.Send(command, cancellationToken);

    if (result.IsFailure)
      return Unauthorized(result.Error);

    return Ok(result.Value);
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
  {
    var command = new RegisterUserCommand(request.Nombre, request.Apellido, request.Email, request.Password);
    var result = await _sender.Send(command, cancellationToken);

    if (result.IsFailure)
      return BadRequest(result.Error);

    return Ok(result.Value);
  }

  [AllowAnonymous]
  [HttpPost("getPagination", Name = "GetUsersPagination")]
  [ProducesResponseType(typeof(PaginationResult<User, UserId>), StatusCodes.Status200OK)]
  public async Task<ActionResult<PagedResults<User, UserId>>> GetPagination(
    [FromQuery] GetUsersPaginationQuery paginationQuery
  )
  {
    var resultados = await _sender.Send(paginationQuery);
    return Ok(resultados);
  }

  [AllowAnonymous]
  [HttpGet("getPaginationDapper", Name = "GetUsersDapperPagination")]
  [ProducesResponseType(typeof(PagedDapperResults<UserPaginationData>), StatusCodes.Status200OK)]
  public async Task<ActionResult<PagedDapperResults<UserPaginationData>>> GetPaginationDapper(
    [FromQuery] GetUsersDapperPaginationQuery paginationQuery
  )
  {
    var resultados = await _sender.Send(paginationQuery);
    return Ok(resultados);
  }
}