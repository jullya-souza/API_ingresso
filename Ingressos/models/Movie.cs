using System.Text.Json.Serialization;

namespace ingressos.Models;

public class Movie
{
    public int Id { get; set; }

    [JsonPropertyName("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [JsonPropertyName("genero")]
    public string Genero { get; set; } = string.Empty;

    [JsonPropertyName("formato")]
    public string Formato { get; set; } = string.Empty;

    [JsonPropertyName("duracao")]
    public string Duracao { get; set; } = string.Empty;

    [JsonPropertyName("imagem")]
    public string Imagem { get; set; } = string.Empty;

    public Movie() { }
}