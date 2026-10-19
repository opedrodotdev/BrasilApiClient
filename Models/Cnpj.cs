using System.Text.Json.Serialization;

namespace BrasilApiClient.Models
{
  public class Cnpj
  {
    [JsonPropertyName("cnpj")]
    public required string Numero { get; set; }

    [JsonPropertyName("razao_social")]
    public required string RazaoSocial { get; set; }

    [JsonPropertyName("nome_fantasia")]
    public string? NomeFantasia { get; set; }

    [JsonPropertyName("municipio")]
    public string? Municipio { get; set; }

    [JsonPropertyName("uf")]
    public string? Uf { get; set; }
  }
}
