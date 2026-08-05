using ControleDeGastos.Data;
using ControleDeGastos.DTOs.Relatorio;
using ControleDeGastos.Models;
using ControleDeGastos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Services;

public class RelatorioService : IRelatorioService {
    private readonly AppDbContext _db;

    public RelatorioService(AppDbContext db) {
        _db = db;
    }

    public async Task<RelatorioGeralResponse> ObterTotaisAsync() {
        var pessoas = await _db
            .Pessoas.Include(p => p.Transacoes)
            .ToListAsync();

        var totaisPorPessoa = pessoas
            .Select(CalcularTotais)
            .ToList();

        return new RelatorioGeralResponse {
            Pessoas = totaisPorPessoa,
            TotalGeralReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas),
            TotalGeralDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas),
            SaldoLiquidoGeral = totaisPorPessoa.Sum(p => p.Saldo)
        };
    }

    private static PessoaTotalResponse CalcularTotais(Pessoa pessoa) {
        var totalReceitas = pessoa
            .Transacoes.Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = pessoa
            .Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        return new PessoaTotalResponse {
            PessoaId = pessoa.Id,
            Nome = pessoa.Nome,
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            Saldo = totalReceitas - totalDespesas
        };
    }
}