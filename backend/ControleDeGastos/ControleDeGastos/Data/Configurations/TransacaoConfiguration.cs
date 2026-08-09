using ControleDeGastos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeGastos.Data.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao> {
    public void Configure(EntityTypeBuilder<Transacao> builder) {
        builder.ToTable("Transacoes");
        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(t => t.Valor)
            .IsRequired();

        builder
            .Property(t => t.Tipo)
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            .IsRequired()
            // Cascade: ao deletar uma Pessoa, todas as suas Transacoes são removidas automaticamente
            .OnDelete(DeleteBehavior.Cascade);
    }
}