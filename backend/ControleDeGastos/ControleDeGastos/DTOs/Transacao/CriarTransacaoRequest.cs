using System.ComponentModel.DataAnnotations;
using ControleDeGastos.Attributes;
using ControleDeGastos.Models;

namespace ControleDeGastos.DTOs.Transacao;

public class CriarTransacaoRequest {
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal? Valor { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [ValidEnumString(typeof(TipoTransacao))]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A pessoa é obrigatória.")]
    [Range(1, long.MaxValue, ErrorMessage = "O identificador da pessoa deve ser um valor positivo.")]
    public long? PessoaId { get; set; }
}