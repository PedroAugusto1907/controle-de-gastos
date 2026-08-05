namespace ControleDeGastos.Models;

public class Transacao {
    public long Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public long PessoaId { get; set; }
    public Pessoa Pessoa { get; set; }
}