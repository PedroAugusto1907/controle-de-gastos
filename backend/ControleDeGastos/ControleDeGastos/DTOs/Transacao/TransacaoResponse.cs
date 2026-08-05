using ControleDeGastos.Models;

namespace ControleDeGastos.DTOs.Transacao;

public class TransacaoResponse {
    public long Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public long PessoaId { get; set; }
    public string PessoaNome { get; set; } = string.Empty;
}