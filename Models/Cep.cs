using System.Text.Json.Serialization;

namespace BrasilApiClient.Models
{
  public class Cep
  {
    [JsonPropertyName("cep")]
    public required string Codigo { get; set; }

    [JsonPropertyName("state")]
    public required string Estado { get; set; }

    [JsonPropertyName("city")]
    public required string Cidade { get; set; }

    [JsonPropertyName("neighborhood")]
    public string? Bairro { get; set; }

    [JsonPropertyName("street")]
    public string? Rua { get; set; }

    [JsonPropertyName("service")]
    public string? Servico { get; set; }
  }
}
