namespace ControleDeGastos.Models;

public class Pessoa {
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    // Regra de negócio: menor de idade = menos de 18 anos
    public bool EhMenorDeIdade => Idade < 18;
}