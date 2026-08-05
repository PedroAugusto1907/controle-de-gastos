namespace ControleDeGastos.DTOs.Pessoa;

public class PessoaResponse {
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public bool EhMenorDeIdade { get; set; }
}