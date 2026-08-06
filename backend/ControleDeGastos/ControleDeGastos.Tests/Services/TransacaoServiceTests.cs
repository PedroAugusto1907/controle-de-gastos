using ControleDeGastos.DTOs.Transacao;
using ControleDeGastos.Exceptions;
using ControleDeGastos.Models;
using ControleDeGastos.Services;
using ControleDeGastos.Tests.Common;

namespace ControleDeGastos.Tests.Services;

public class TransacaoServiceTests {
    [Fact]
    public async Task CriarAsync_PessoaMenorDeIdade_ComTipoReceita_DeveLancarBusinessRuleException() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Criança", Idade = 10 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        var service = new TransacaoService(db);
        var request = new CriarTransacaoRequest { Descricao = "Mesada", Valor = 50, Tipo = "Receita", PessoaId = pessoa.Id };

        // Act
        var act = () => service.CriarAsync(request);

        // Assert
        await Assert.ThrowsAsync<BusinessRuleException>(act);
    }

    [Fact]
    public async Task CriarAsync_PessoaMenorDeIdade_ComTipoDespesa_DeveCriarComSucesso() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Criança", Idade = 10 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        var service = new TransacaoService(db);
        var request = new CriarTransacaoRequest { Descricao = "Lanche", Valor = 15, Tipo = "Despesa", PessoaId = pessoa.Id };

        // Act
        var resultado = await service.CriarAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(TipoTransacao.Despesa, resultado.Tipo);
        Assert.Equal("Criança", resultado.PessoaNome);
    }

    [Fact]
    public async Task CriarAsync_PessoaMaiorDeIdade_ComTipoReceita_DeveCriarComSucesso() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Adulto", Idade = 30 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        var service = new TransacaoService(db);
        var request = new CriarTransacaoRequest { Descricao = "Salário", Valor = 3000, Tipo = "Receita", PessoaId = pessoa.Id };

        // Act
        var resultado = await service.CriarAsync(request);

        // Assert
        Assert.Equal(TipoTransacao.Receita, resultado.Tipo);
    }

    [Fact]
    public async Task CriarAsync_PessoaInexistente_DeveLancarNotFoundException() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var service = new TransacaoService(db);

        var request = new CriarTransacaoRequest { Descricao = "Aluguel", Valor = 1200, Tipo = "Despesa", PessoaId = 999 };

        // Act
        var act = () => service.CriarAsync(request);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async Task ListarAsync_DeveRetornarTransacoesComNomeDaPessoa() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Maria", Idade = 25 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        db.Transacoes.Add(new Transacao { Descricao = "Freelance", Valor = 500, Tipo = TipoTransacao.Receita, PessoaId = pessoa.Id });
        await db.SaveChangesAsync();

        var service = new TransacaoService(db);

        // Act
        var resultado = (await service.ListarAsync()).ToList();

        // Assert
        var transacao = Assert.Single(resultado);
        Assert.Equal("Maria", transacao.PessoaNome);
    }
}