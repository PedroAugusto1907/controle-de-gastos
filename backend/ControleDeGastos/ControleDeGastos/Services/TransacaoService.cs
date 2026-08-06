using ControleDeGastos.Data;
using ControleDeGastos.DTOs.Transacao;
using ControleDeGastos.Exceptions;
using ControleDeGastos.Models;
using ControleDeGastos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Services;

public class TransacaoService : ITransacaoService {
    private readonly AppDbContext _db;

    public TransacaoService(AppDbContext db) {
        _db = db;
    }

    public async Task<IEnumerable<TransacaoResponse>> ListarAsync() {
        return await _db
            .Transacoes.Include(t => t.Pessoa)
            .Select(t => new TransacaoResponse {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo.ToString(),
                PessoaId = t.PessoaId,
                PessoaNome = t.Pessoa.Nome
            })
            .ToListAsync();
    }

    public async Task<TransacaoResponse> CriarAsync(CriarTransacaoRequest request) {
        var pessoa = await _db.Pessoas.FindAsync(request.PessoaId!.Value) ?? throw new NotFoundException($"Pessoa com Id {request.PessoaId} não encontrada.");

        // Regra de negócio: pessoas menores de idade (< 18 anos) só podem ter despesas cadastradas
        if (pessoa.EhMenorDeIdade && request.Tipo == TipoTransacao.Receita)
            throw new BusinessRuleException("Pessoas menores de idade só podem ter despesas cadastradas.");

        var transacao = new Transacao {
            Descricao = request.Descricao, Valor = request.Valor!.Value, Tipo = request.Tipo!.Value, PessoaId = request.PessoaId!.Value
        };

        _db.Transacoes.Add(transacao);
        await _db.SaveChangesAsync();

        return new TransacaoResponse {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo.ToString(),
            PessoaId = transacao.PessoaId,
            PessoaNome = pessoa.Nome
        };
    }
}