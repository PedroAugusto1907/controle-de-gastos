using ControleDeGastos.DTOs.Pessoa;
using ControleDeGastos.Exceptions;
using ControleDeGastos.Models;
using ControleDeGastos.Services;
using ControleDeGastos.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Tests.Services;

public class PessoaServiceTests {
    [Fact]
    public async Task CriarAsync_DeveCadastrarPessoaComSucesso() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var service = new PessoaService(db);

        var request = new CriarPessoaRequest { Nome = "João", Idade = 30 };

        // Act
        var resultado = await service.CriarAsync(request);

        // Assert
        Assert.True(resultado.Id > 0);
        Assert.False(resultado.EhMenorDeIdade);
    }

    [Fact]
    public async Task CriarAsync_PessoaMenorDeIdade_DeveMarcarEhMenorDeIdade() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var service = new PessoaService(db);

        var request = new CriarPessoaRequest { Nome = "Pedro", Idade = 15 };

        // Act
        var resultado = await service.CriarAsync(request);

        // Assert
        Assert.True(resultado.EhMenorDeIdade);
    }

    [Fact]
    public async Task DeletarAsync_PessoaComTransacoes_DeveRemoverTransacoesAssociadas() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Ana", Idade = 40 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        db.Transacoes.AddRange(new Transacao { Descricao = "Aluguel", Valor = 1000, Tipo = TipoTransacao.Despesa, PessoaId = pessoa.Id },
            new Transacao { Descricao = "Salário", Valor = 5000, Tipo = TipoTransacao.Receita, PessoaId = pessoa.Id });

        await db.SaveChangesAsync();

        var service = new PessoaService(db);

        // Act
        await service.DeletarAsync(pessoa.Id);

        // Assert
        var transacoesRestantes = await db
            .Transacoes.Where(t => t.PessoaId == pessoa.Id)
            .ToListAsync();

        Assert.Empty(transacoesRestantes);
        Assert.Null(await db.Pessoas.FindAsync(pessoa.Id));
    }

    [Fact]
    public async Task DeletarAsync_PessoaInexistente_DeveLancarNotFoundException() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var service = new PessoaService(db);

        // Act
        var act = () => service.DeletarAsync(999);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}