using System.Text.Json.Serialization;

namespace ingressos.Models;

public class User
{
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("cpf")]
    public string Cpf { get; set; } = string.Empty;

    [JsonPropertyName("nascimento")]
    public DateTime Nascimento { get; set; }

    [JsonPropertyName("senha")]
    public string Senha { get; set; } = string.Empty;

    public User(string nome, string email, string cpf, DateTime nascimento, string senha)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        Nascimento = nascimento;
        Senha = senha;
    }

    public User() { }
}