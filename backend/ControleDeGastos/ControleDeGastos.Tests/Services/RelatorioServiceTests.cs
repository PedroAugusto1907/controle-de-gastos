using ControleDeGastos.Models;
using ControleDeGastos.Services;
using ControleDeGastos.Tests.Common;

namespace ControleDeGastos.Tests.Services;

public class RelatorioServiceTests {
    [Fact]
    public async Task ObterTotaisAsync_DeveCalcularReceitasDespesasESaldoPorPessoa() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Carlos", Idade = 35 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        db.Transacoes.AddRange(new Transacao { Descricao = "Salário", Valor = 5000, Tipo = TipoTransacao.Receita, PessoaId = pessoa.Id },
            new Transacao { Descricao = "Aluguel", Valor = 1500, Tipo = TipoTransacao.Despesa, PessoaId = pessoa.Id },
            new Transacao { Descricao = "Mercado", Valor = 800, Tipo = TipoTransacao.Despesa, PessoaId = pessoa.Id });

        await db.SaveChangesAsync();

        var service = new RelatorioService(db);

        // Act
        var resultado = await service.ObterTotaisAsync();

        // Assert
        var totalPessoa = Assert.Single(resultado.Pessoas);

        Assert.Equal(5000, totalPessoa.TotalReceitas);
        Assert.Equal(2300, totalPessoa.TotalDespesas);
        Assert.Equal(2700, totalPessoa.Saldo);
    }

    [Fact]
    public async Task ObterTotaisAsync_PessoaSemTransacoes_DeveAparecerComTotaisZerados() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        db.Pessoas.Add(new Pessoa { Nome = "Sem Transações", Idade = 20 });
        await db.SaveChangesAsync();

        var service = new RelatorioService(db);

        // Act
        var resultado = await service.ObterTotaisAsync();

        // Assert
        var totalPessoa = Assert.Single(resultado.Pessoas);

        Assert.Equal(0, totalPessoa.TotalReceitas);
        Assert.Equal(0, totalPessoa.TotalDespesas);
        Assert.Equal(0, totalPessoa.Saldo);
    }

    [Fact]
    public async Task ObterTotaisAsync_DespesasMaioresQueReceitas_DeveRetornarSaldoNegativo() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa = new Pessoa { Nome = "Endividado", Idade = 28 };
        db.Pessoas.Add(pessoa);
        await db.SaveChangesAsync();

        db.Transacoes.AddRange(new Transacao { Descricao = "Freelance", Valor = 200, Tipo = TipoTransacao.Receita, PessoaId = pessoa.Id },
            new Transacao { Descricao = "Cartão", Valor = 800, Tipo = TipoTransacao.Despesa, PessoaId = pessoa.Id });

        await db.SaveChangesAsync();

        var service = new RelatorioService(db);

        // Act
        var resultado = await service.ObterTotaisAsync();

        // Assert
        var totalPessoa = Assert.Single(resultado.Pessoas);

        Assert.Equal(-600m, totalPessoa.Saldo);
    }

    [Fact]
    public async Task ObterTotaisAsync_TotalGeral_DeveSomarTodasAsPessoas() {
        // Arrange
        await using var db = TestDbContextFactory.Create();
        var pessoa1 = new Pessoa { Nome = "Pessoa 1", Idade = 30 };
        var pessoa2 = new Pessoa { Nome = "Pessoa 2", Idade = 25 };
        db.Pessoas.AddRange(pessoa1, pessoa2);
        await db.SaveChangesAsync();

        db.Transacoes.AddRange(new Transacao { Descricao = "Salário", Valor = 3000, Tipo = TipoTransacao.Receita, PessoaId = pessoa1.Id },
            new Transacao { Descricao = "Aluguel", Valor = 1000, Tipo = TipoTransacao.Despesa, PessoaId = pessoa1.Id },
            new Transacao { Descricao = "Salário", Valor = 2000, Tipo = TipoTransacao.Receita, PessoaId = pessoa2.Id },
            new Transacao { Descricao = "Mercado", Valor = 500, Tipo = TipoTransacao.Despesa, PessoaId = pessoa2.Id });

        await db.SaveChangesAsync();

        var service = new RelatorioService(db);

        // Act
        var resultado = await service.ObterTotaisAsync();

        // Assert
        Assert.Equal(5000, resultado.TotalGeralReceitas);
        Assert.Equal(1500, resultado.TotalGeralDespesas);
        Assert.Equal(3500, resultado.SaldoLiquidoGeral);
    }
}