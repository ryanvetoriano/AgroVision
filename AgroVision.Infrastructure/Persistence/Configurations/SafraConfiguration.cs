using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class SafraConfiguration : IEntityTypeConfiguration<Safra>
{
    public void Configure(EntityTypeBuilder<Safra> builder)
    {
        builder.ToTable("TB_SAFRA_GS");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("ID_SAFRA")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.PlantacaoId)
            .HasColumnName("ID_PLANTACAO")
            .IsRequired();

        builder.Property(s => s.DataColheita)
            .HasColumnName("DATA_COLHEITA")
            .IsRequired();

        builder.Property(s => s.QuantidadeColhida)
            .HasColumnName("QTD_COLHIDA")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.Property(s => s.ReceitaEstimada)
            .HasColumnName("RECEITA_ESTIMADA")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.Property(s => s.PerdaEstimada)
            .HasColumnName("PERDA_ESTIMADA")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.HasOne(s => s.Plantacao)
            .WithMany(p => p.Safras)
            .HasForeignKey(s => s.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}