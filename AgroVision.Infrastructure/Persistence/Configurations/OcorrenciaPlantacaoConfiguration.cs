using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class OcorrenciaPlantacaoConfiguration : IEntityTypeConfiguration<OcorrenciaPlantacao>
{
    public void Configure(EntityTypeBuilder<OcorrenciaPlantacao> builder)
    {
        builder.ToTable("TB_OCORRENCIA_PLANTACAO_GS");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasColumnName("ID_OCORRENCIA")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.PlantacaoId)
            .HasColumnName("ID_PLANTACAO")
            .IsRequired();

        builder.Property(o => o.AnaliseDroneId)
            .HasColumnName("ID_ANALISE_DRONE")
            .IsRequired(false);

        builder.Property(o => o.TipoOcorrencia)
            .HasColumnName("TIPO_OCORRENCIA")
            .HasConversion<string>()
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(o => o.NivelRisco)
            .HasColumnName("NIVEL_RISCO")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(o => o.Descricao)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(o => o.DataOcorrencia)
            .HasColumnName("DATA_OCORRENCIA")
            .IsRequired();

        builder.Property(o => o.Resolvida)
            .HasColumnName("RESOLVIDA")
            .HasColumnType("NUMBER(1)")
            .IsRequired();

        builder.HasOne(o => o.Plantacao)
            .WithMany(p => p.Ocorrencias)
            .HasForeignKey(o => o.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.AnaliseDrone)
            .WithMany(a => a.Ocorrencias)
            .HasForeignKey(o => o.AnaliseDroneId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}