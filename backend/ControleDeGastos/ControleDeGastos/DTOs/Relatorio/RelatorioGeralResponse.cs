namespace ControleDeGastos.DTOs.Relatorio;

public class RelatorioGeralResponse {
    public List<PessoaTotalResponse> Pessoas { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoLiquidoGeral { get; set; }
}