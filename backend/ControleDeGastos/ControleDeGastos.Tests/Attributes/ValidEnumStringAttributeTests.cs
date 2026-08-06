using System.ComponentModel.DataAnnotations;
using ControleDeGastos.Attributes;
using ControleDeGastos.Models;

namespace ControleDeGastos.Tests.Attributes;

public class ValidEnumStringAttributeTests {
    private readonly ValidEnumStringAttribute _attribute = new(typeof(TipoTransacao));

    [Theory]
    [InlineData("Despesa")]
    [InlineData("Receita")]
    [InlineData("despesa")] 
    [InlineData("RECEITA")]
    public void IsValid_ComValorValido_DeveRetornarSucesso(string valor) {
        // Arrange
        var context = new ValidationContext(new object());

        // Act
        var resultado = _attribute.GetValidationResult(valor, context);

        // Assert
        Assert.Equal(ValidationResult.Success, resultado);
    }

    [Theory]
    [InlineData("Investimento")]
    [InlineData("xyz")]
    [InlineData("123")]
    public void IsValid_ComValorInvalido_DeveRetornarErroComMensagemDosValoresAceitos(string valor) {
        // Arrange
        var context = new ValidationContext(new object());
        var valoresEsperados = string.Join(", ", Enum.GetNames<TipoTransacao>());

        // Act
        var resultado = _attribute.GetValidationResult(valor, context);

        // Assert
        Assert.NotEqual(ValidationResult.Success, resultado);
        Assert.Contains(valoresEsperados, resultado!.ErrorMessage);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValid_ComValorVazioOuNulo_DeveRetornarSucesso(string? valor) {
        // Arrange
        var context = new ValidationContext(new object());

        // Act
        var resultado = _attribute.GetValidationResult(valor, context);

        // Assert
        Assert.Equal(ValidationResult.Success, resultado);
    }
}