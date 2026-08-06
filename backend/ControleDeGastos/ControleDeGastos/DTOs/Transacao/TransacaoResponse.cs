namespace ControleDeGastos.DTOs.Transacao;

public class TransacaoResponse {
    public long Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public long PessoaId { get; set; }
    public string PessoaNome { get; set; } = string.Empty;
}