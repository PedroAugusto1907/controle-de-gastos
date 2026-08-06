using ControleDeGastos.Data;
using ControleDeGastos.DTOs.Pessoa;
using ControleDeGastos.Exceptions;
using ControleDeGastos.Models;
using ControleDeGastos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Services;

public class PessoaService : IPessoaService {
    private readonly AppDbContext _db;

    public PessoaService(AppDbContext db) {
        _db = db;
    }

    public async Task<IEnumerable<PessoaResponse>> ListarAsync() {
        return await _db
            .Pessoas.Select(p => new PessoaResponse { Id = p.Id, Nome = p.Nome, Idade = p.Idade, EhMenorDeIdade = p.EhMenorDeIdade })
            .ToListAsync();
    }

    public async Task<PessoaResponse> ObterPorIdAsync(long id) {
        var pessoa = await _db
            .Pessoas.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException($"Pessoa com Id {id} não encontrada.");

        return new PessoaResponse { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, EhMenorDeIdade = pessoa.EhMenorDeIdade };
    }

    public async Task<PessoaResponse> CriarAsync(CriarPessoaRequest request) {
        var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade!.Value };

        _db.Pessoas.Add(pessoa);
        await _db.SaveChangesAsync();

        return new PessoaResponse { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, EhMenorDeIdade = pessoa.EhMenorDeIdade };
    }

    public async Task DeletarAsync(long id) {
        var pessoa = await _db.Pessoas.FindAsync(id) ?? throw new NotFoundException($"Pessoa com Id {id} não encontrada.");

        _db.Pessoas.Remove(pessoa);
        await _db.SaveChangesAsync();
    }
}