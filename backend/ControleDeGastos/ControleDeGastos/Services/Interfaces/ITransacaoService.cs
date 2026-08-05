using ControleDeGastos.DTOs.Transacao;

namespace ControleDeGastos.Services.Interfaces;

public interface ITransacaoService {
    Task<IEnumerable<TransacaoResponse>> ListarAsync();

    Task<TransacaoResponse> CriarAsync(CriarTransacaoRequest request);
}