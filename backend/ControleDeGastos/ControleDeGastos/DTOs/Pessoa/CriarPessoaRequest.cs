using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Pessoa;

public class CriarPessoaRequest {
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é obrigatória.")]
    [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150.")]
    public int Idade { get; set; }
}