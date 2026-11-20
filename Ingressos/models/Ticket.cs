namespace ingressos.Models;

public class Ticket
{
    public int Id { get; private set; }
    public string Filme { get; private set; }
    public string Horario { get; private set; }
    public int Quantidade { get; private set; }
    public string EmailUsuario { get; private set; }
    public decimal ValorTotal { get; private set; }

    public Ticket(string filme, string horario, int quantidade, string emailUsuario)
    {
        Filme = filme;
        Horario = horario;
        Quantidade = quantidade;
        EmailUsuario = emailUsuario;
        ValorTotal = CalcularPrecoTotal();
    }

    // Regra de negócio encapsulada (Princípio da POO)
    private decimal CalcularPrecoTotal()
    {
        decimal precoUnitario = 30.00m; // Preço padrão do ingresso no seu front
        return Quantidade * precoUnitario;
    }
}