using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ingressos.Models;

namespace ingressos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly IConfiguration _configuration;

    // Correção do nome da variável de configuration (estava com typo no seu código original)
    public TicketController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult ComprarIngresso([FromBody] TicketDto dto)
    {
        // Instanciando o objeto aplicando POO e regras encapsuladas
        var novoIngresso = new Ticket(dto.Filme, dto.Horario, dto.Quantidade, dto.EmailUsuario);

        string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "INSERT INTO ingressos (filme, horario, quantidade, email_usuario, valor_total) VALUES (@filme, @horario, @quantidade, @emailUsuario, @valorTotal)";

            using (var cmd = new MySqlCommand(query, conexao))
            {
                cmd.Parameters.AddWithValue("@filme", novoIngresso.Filme);
                cmd.Parameters.AddWithValue("@horario", novoIngresso.Horario);
                cmd.Parameters.AddWithValue("@quantidade", novoIngresso.Quantidade);
                cmd.Parameters.AddWithValue("@emailUsuario", novoIngresso.EmailUsuario);
                cmd.Parameters.AddWithValue("@valorTotal", novoIngresso.ValorTotal);

                cmd.ExecuteNonQuery();
            }
        }

        return Ok(new { mensagem = "Ingresso comprado com sucesso!", total = novoIngresso.ValorTotal });
    }
}

// Dto auxiliar para receber os dados vindos do front-end
public class TicketDto
{
    public string Filme { get; set; } = string.Empty;
    public string Horario { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string EmailUsuario { get; set; } = string.Empty;
}