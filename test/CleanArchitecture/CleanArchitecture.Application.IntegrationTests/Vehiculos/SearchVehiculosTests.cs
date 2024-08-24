using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Vehiculos;

public class SearchVehiculos : BaseIntegrationTest
{
  public SearchVehiculos(IntegrationTestWebAppFactory factory) : base(factory) { }

  [Fact]
  public async Task SearchVehiculos_Should_Return_EmptyList_WhenDateRangeInvalid()
  {
    var query = new SearchVehiculosQuery(
      new DateOnly(2023, 1, 1),
      new DateOnly(2022, 1, 1)
    );

    var resultado = await Sender.Send(query);

    resultado.Value.Should().BeEmpty();
  }

  [Fact]
  public async Task SearchVehiculos_Should_ReturnVehiculos_WhenDateRangeIsValid()
  {
    var query = new SearchVehiculosQuery(
      new DateOnly(2022, 1, 1),
      new DateOnly(2023, 1, 1)
    );

    var resultado = await Sender.Send(query);

    resultado.Value.Should().NotBeEmpty();
  }
}