using ControleDeGastos.DTOs.Relatorio;

namespace ControleDeGastos.Services.Interfaces;

public interface IRelatorioService {
    Task<RelatorioGeralResponse> ObterTotaisAsync();
}