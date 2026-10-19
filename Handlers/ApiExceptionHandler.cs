using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BrasilApiClient.Handlers
{
  public class ApiExceptionHandler : IExceptionHandler
  {
    private readonly ILogger<ApiExceptionHandler> _logger;

    public ApiExceptionHandler(ILogger<ApiExceptionHandler> logger)
    {
      _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken ct)
    {
      ProblemDetails problem;

      if (exception is HttpRequestException)
      {
        problem = new ProblemDetails
        {
          Status = StatusCodes.Status502BadGateway,
          Title = "Falha ao consultar a BrasilAPI",
          Detail = "O serviço externo está indisponível ou retornou um erro."
        };
      }
      else if (exception is TaskCanceledException && !ct.IsCancellationRequested)
      {
        problem = new ProblemDetails
        {
          Status = StatusCodes.Status504GatewayTimeout,
          Title = "Tempo de resposta excedido",
          Detail = "A BrasilAPI demorou demais para responder."
        };
      }
      else
      {
        return false; // outras exceções seguem o fluxo padrão
      }

      _logger.LogError(exception, "Erro ao chamar a API externa");

      httpContext.Response.StatusCode = problem.Status!.Value;
      await httpContext.Response.WriteAsJsonAsync(problem, ct);
      return true;
    }
  }
}
