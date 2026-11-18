using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ingressos.Models;

namespace ingressos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public MovieController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Listar todos os filmes (GET: api/movie)
    [HttpGet]
    public IActionResult ListarFilmes()
    {
        var filmes = new List<Movie>();
        string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "SELECT * FROM filmes";

            using (var cmd = new MySqlCommand(query, conexao))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filmes.Add(new Movie
                        {
                            Id = reader.GetInt32("id"),
                            Titulo = reader.GetString("titulo"),
                            Genero = reader.GetString("genero"),
                            Formato = reader.GetString("formato"),
                            Duracao = reader.GetString("duracao"),
                            Imagem = reader.GetString("imagem")
                        });
                    }
                }
            }
        }

        return Ok(filmes);
    }

    // Cadastrar novo filme (POST: api/movie)
    [HttpPost]
    public IActionResult CadastrarFilme([FromBody] Movie filme)
    {
        try
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";

            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                string query = "INSERT INTO filmes (titulo, genero, formato, duracao, imagem) VALUES (@titulo, @genero, @formato, @duracao, @imagem)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@titulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("@genero", filme.Genero);
                    cmd.Parameters.AddWithValue("@formato", filme.Formato);
                    cmd.Parameters.AddWithValue("@duracao", filme.Duracao);
                    cmd.Parameters.AddWithValue("@imagem", filme.Imagem);

                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(new { mensagem = "Filme cadastrado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }
}