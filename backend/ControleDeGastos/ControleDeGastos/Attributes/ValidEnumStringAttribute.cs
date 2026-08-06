using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.Attributes;

public class ValidEnumStringAttribute : ValidationAttribute {
    private readonly Type _enumType;

    public ValidEnumStringAttribute(Type enumType) {
        if (!enumType.IsEnum)
            throw new ArgumentException($"{enumType.Name} não é um enum.", nameof(enumType));

        _enumType = enumType;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
        if (value is not string texto || string.IsNullOrWhiteSpace(texto))
            return ValidationResult.Success;

        if (Enum.TryParse(_enumType, texto, true, out _))
            return ValidationResult.Success;

        var valoresValidos = string.Join(", ", Enum.GetNames(_enumType));
        return new ValidationResult($"Valor inválido para '{validationContext.MemberName}'. Valores aceitos: {valoresValidos}.");
    }
}