namespace ControleDeGastos.DTOs.Relatorio;

public class PessoaTotalResponse {
    public long PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}