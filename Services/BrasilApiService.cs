using System.Net;
using System.Net.Http.Json;
using BrasilApiClient.Models;

namespace BrasilApiClient.Services
{
  public class BrasilApiService
  {
    private readonly HttpClient _http;
    public BrasilApiService(HttpClient http)
    {
      _http = http;
    }

    public Task<Cep?> BuscarCepAsync(string cep, CancellationToken ct = default) =>
        GetOrNullAsync<Cep>($"cep/v2/{cep}", ct);
    public Task<Cnpj?> BuscarCnpjAsync(string cnpj, CancellationToken ct = default) =>
        GetOrNullAsync<Cnpj>($"cnpj/v1/{cnpj}", ct);
    private async Task<T?> GetOrNullAsync<T>(string url, CancellationToken ct)
    {
      var response = await _http.GetAsync(url, ct);

      if (response.StatusCode == HttpStatusCode.NotFound)
        return default;

      response.EnsureSuccessStatusCode();

      return await response.Content.ReadFromJsonAsync<T>(ct);
    }
  }
}
