using BrasilApiClient.Services;
using Microsoft.AspNetCore.Mvc;
using BrasilApiClient.Models;

namespace BrasilApiClient.Controllers
{
  [ApiController]
  public class BrasilApiController : ControllerBase
  {
    private readonly BrasilApiService _api;

    public BrasilApiController(BrasilApiService api)
    {
      _api = api;
    }

    [HttpGet("cep/{cep}")]
    [EndpointSummary("Consulta um CEP")]
    [EndpointDescription("Busca o endereço de um CEP na BrasilAPI. Informe 8 dígitos numéricos, sem hífen.")]
    [ProducesResponseType<Cep>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status502BadGateway)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status504GatewayTimeout)]
    public async Task<IActionResult> GetCep(string cep, CancellationToken ct)
    {
      if (cep.Length != 8 || !cep.All(char.IsDigit))
        return Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "CEP inválido",
            detail: "O CEP deve ter 8 dígitos numéricos.");

      var resultado = await _api.BuscarCepAsync(cep, ct);

      return resultado is null
          ? Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "CEP não encontrado",
            detail: "Não foi encontrado um endereço para o CEP informado.")
          : Ok(resultado);
    }

    [HttpGet("cnpj/{cnpj}")]
    [EndpointSummary("Consulta um CNPJ")]
    [EndpointDescription("Busca o endereço de um CNPJ na BrasilAPI. Informe 14 dígitos numéricos, sem hífen.")]
    [ProducesResponseType<Cnpj>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status502BadGateway)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status504GatewayTimeout)]
    public async Task<IActionResult> GetCnpj(string cnpj, CancellationToken ct)
    {
      if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
        return Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "CNPJ inválido",
            detail: "O CNPJ deve ter 14 dígitos numéricos.");

      var resultado = await _api.BuscarCnpjAsync(cnpj, ct);

      return resultado is null
          ? Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "CNPJ não encontrado",
            detail: "Não foi encontrada uma empresa para o CNPJ informado.")
          : Ok(resultado);
    }
  }
}
