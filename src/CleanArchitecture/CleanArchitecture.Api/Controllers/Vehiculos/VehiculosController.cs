using CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;
using CleanArchitecture.Application.Vehiculos.ReportVehiculoPdf;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace CleanArchitecture.Api.Controllers.Vehiculos;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase
{
  private readonly ISender _sender;
  public VehiculosController(ISender sender)
  {
    _sender = sender;
  }

  [AllowAnonymous]
  [HttpGet("reporte")]
  public async Task<IActionResult> Reporte(
    CancellationToken cancellationToken,
    string modelo = "")
  {
    var query = new ReportVehiculoPdfQuery(modelo);
    var resultados = await _sender.Send(query, cancellationToken);
    byte[] pdfbytes = resultados.Value.GeneratePdf();

    return File(pdfbytes, "application/pdf");
  }

  [HasPermission(PermissionEnum.ReadUser)]
  [HttpGet("search")]
  public async Task<IActionResult> SearchVehiculos(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
  {
    var query = new SearchVehiculosQuery(startDate, endDate);
    var resultados = await _sender.Send(query, cancellationToken);
    return Ok(resultados.Value);
  }

  [AllowAnonymous]
  [HttpGet("getPagination", Name = "Pagination vehiculos")]
  [ProducesResponseType(typeof(PaginationResult<Vehiculo, VehiculoId>), StatusCodes.Status200OK)]
  public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetPaginationVehiculo(
    [FromQuery] GetVehiculosByPaginationQuery request)
  {
    var resultados = await _sender.Send(request);
    return Ok(resultados.Value);
  }
}