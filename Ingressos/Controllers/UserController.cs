using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ingressos.Models;

namespace ingressos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CadastrarUsuario([FromBody] User usuarioDto)
    {
        try
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";

            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                string query = "INSERT INTO usuarios (nome, email, cpf, nascimento, senha) VALUES (@nome, @email, @cpf, @nascimento, @senha)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", usuarioDto.Nome);
                    cmd.Parameters.AddWithValue("@email", usuarioDto.Email);
                    cmd.Parameters.AddWithValue("@cpf", usuarioDto.Cpf);
                    cmd.Parameters.AddWithValue("@nascimento", usuarioDto.Nascimento);
                    cmd.Parameters.AddWithValue("@senha", usuarioDto.Senha);

                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
        }
        catch (Exception ex)
        {
            // Retorna o erro detalhado para sabermos exatamente o que falhou
            return StatusCode(500, new { erro = ex.Message });
        }
    }
}