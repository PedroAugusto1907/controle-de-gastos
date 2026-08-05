using ControleDeGastos.DTOs.Pessoa;

namespace ControleDeGastos.Services.Interfaces;

public interface IPessoaService {
    Task<IEnumerable<PessoaResponse>> ListarAsync();

    Task<PessoaResponse> CriarAsync(CriarPessoaRequest request);

    Task DeletarAsync(long id);
}