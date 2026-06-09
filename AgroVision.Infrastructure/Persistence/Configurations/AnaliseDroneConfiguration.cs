using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class AnaliseDroneConfiguration : IEntityTypeConfiguration<AnaliseDrone>
{
    public void Configure(EntityTypeBuilder<AnaliseDrone> builder)
    {
        builder.ToTable("TB_ANALISE_DRONE_GS");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("ID_ANALISE_DRONE")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.PlantacaoId)
            .HasColumnName("ID_PLANTACAO")
            .IsRequired();

        builder.Property(a => a.MissaoDroneId)
            .HasColumnName("ID_MISSAO_DRONE")
            .IsRequired(false);

        builder.Property(a => a.DataAnalise)
            .HasColumnName("DATA_ANALISE")
            .IsRequired();

        builder.Property(a => a.IndiceSaude)
            .HasColumnName("INDICE_SAUDE")
            .HasColumnType("NUMBER(5,2)")
            .IsRequired();

        builder.Property(a => a.UmidadeSolo)
            .HasColumnName("UMIDADE_SOLO")
            .HasColumnType("NUMBER(5,2)")
            .IsRequired();

        builder.Property(a => a.TemperaturaMedia)
            .HasColumnName("TEMPERATURA_MEDIA")
            .HasColumnType("NUMBER(5,2)")
            .IsRequired();

        builder.Property(a => a.IndiceVegetacaoNdvi)
            .HasColumnName("INDICE_VEGETACAO_NDVI")
            .HasColumnType("NUMBER(5,2)")
            .IsRequired();

        builder.Property(a => a.AreaAfetadaPercentual)
            .HasColumnName("AREA_AFETADA_PERCENTUAL")
            .HasColumnType("NUMBER(5,2)")
            .IsRequired();

        builder.Property(a => a.PragasDetectadas)
            .HasColumnName("PRAGAS_DETECTADAS")
            .HasColumnType("NUMBER(1)")
            .IsRequired();

        builder.Property(a => a.NivelRisco)
            .HasColumnName("NIVEL_RISCO")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.StatusAnalise)
            .HasColumnName("STATUS_ANALISE")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.Recomendacao)
            .HasColumnName("RECOMENDACAO")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.ObservacaoImagem)
            .HasColumnName("OBSERVACAO_IMAGEM")
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasOne(a => a.Plantacao)
            .WithMany(p => p.AnalisesDrone)
            .HasForeignKey(a => a.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.MissaoDrone)
            .WithOne()
            .HasForeignKey<AnaliseDrone>(a => a.MissaoDroneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(a => a.Ocorrencias)
            .WithOne(o => o.AnaliseDrone)
            .HasForeignKey(o => o.AnaliseDroneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(a => a.Recomendacoes)
            .WithOne(r => r.AnaliseDrone)
            .HasForeignKey(r => r.AnaliseDroneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}