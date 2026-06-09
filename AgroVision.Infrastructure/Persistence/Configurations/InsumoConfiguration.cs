using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class InsumoConfiguration : IEntityTypeConfiguration<Insumo>
{
    public void Configure(EntityTypeBuilder<Insumo> builder)
    {
        builder.ToTable("TB_INSUMO_GS");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnName("ID_INSUMO")
            .ValueGeneratedOnAdd();

        builder.Property(i => i.PlantacaoId)
            .HasColumnName("ID_PLANTACAO")
            .IsRequired();

        builder.Property(i => i.NomeInsumo)
            .HasColumnName("NOME_INSUMO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.UnidadeMedida)
            .HasColumnName("UNIDADE_MEDIDA")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.QuantidadeEstoque)
            .HasColumnName("QTD_ESTOQUE")
            .HasColumnType("NUMBER(10,2)")
            .IsRequired();

        builder.Property(i => i.EstoqueMinimo)
            .HasColumnName("ESTOQUE_MINIMO")
            .HasColumnType("NUMBER(10,2)")
            .IsRequired();

        builder.Property(i => i.DataUltimaAplicacao)
            .HasColumnName("DATA_ULTIMA_APLICACAO")
            .IsRequired(false);

        builder.HasOne(i => i.Plantacao)
            .WithMany(p => p.Insumos)
            .HasForeignKey(i => i.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}