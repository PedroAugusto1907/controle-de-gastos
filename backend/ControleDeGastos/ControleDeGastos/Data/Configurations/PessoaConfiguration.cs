using ControleDeGastos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeGastos.Data.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa> {
    public void Configure(EntityTypeBuilder<Pessoa> builder) {
        builder.ToTable("Pessoas");
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(p => p.Idade)
            .IsRequired();

        // Propriedade calculada (não mapeada no banco, apenas derivada de Idade)
        builder.Ignore(p => p.EhMenorDeIdade);
    }
}