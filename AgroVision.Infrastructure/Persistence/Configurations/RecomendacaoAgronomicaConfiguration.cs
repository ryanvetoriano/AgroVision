using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class RecomendacaoAgronomicaConfiguration : IEntityTypeConfiguration<RecomendacaoAgronomica>
{
    public void Configure(EntityTypeBuilder<RecomendacaoAgronomica> builder)
    {
        builder.ToTable("TB_RECOMENDACAO_AGRONOMICA_GS");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("ID_RECOMENDACAO")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.AnaliseDroneId)
            .HasColumnName("ID_ANALISE_DRONE")
            .IsRequired();

        builder.Property(r => r.Titulo)
            .HasColumnName("TITULO")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Descricao)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.Prioridade)
            .HasColumnName("PRIORIDADE")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(r => r.DataGeracao)
            .HasColumnName("DATA_GERACAO")
            .IsRequired();

        builder.Property(r => r.Concluida)
            .HasColumnName("CONCLUIDA")
            .HasColumnType("NUMBER(1)")
            .IsRequired();

        builder.HasOne(r => r.AnaliseDrone)
            .WithMany(a => a.Recomendacoes)
            .HasForeignKey(r => r.AnaliseDroneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}