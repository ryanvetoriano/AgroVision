using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class PlantacaoConfiguration : IEntityTypeConfiguration<Plantacao>
{
    public void Configure(EntityTypeBuilder<Plantacao> builder)
    {
        builder.ToTable("TB_PLANTACOES_GS");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("ID_PLANTACAO")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UsuarioId)
            .HasColumnName("ID_USER")
            .IsRequired();

        builder.Property(p => p.TipoPlantio)
            .HasColumnName("TIPO_PLANTIO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Cultura)
            .HasColumnName("CULTURA")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.AreaPlantio)
            .HasColumnName("AREA_PLANTIO")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.Property(p => p.ProdutividadeEstimada)
            .HasColumnName("PRODUTIVIDADE_ESTIMADA")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.Property(p => p.DataPlantio)
            .HasColumnName("DATA_PLANTIO")
            .IsRequired();

        builder.Property(p => p.LocalPlantio)
            .HasColumnName("LOCAL_PLANTIO")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.StatusPlantio)
            .HasColumnName("STATUS_PLANTIO")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Ativa)
            .HasColumnName("ATIVA")
            .HasColumnType("NUMBER(1)")
            .IsRequired();

        builder.HasOne(p => p.Usuario)
            .WithMany(u => u.Plantacoes)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Safras)
            .WithOne(s => s.Plantacao)
            .HasForeignKey(s => s.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Insumos)
            .WithOne(i => i.Plantacao)
            .HasForeignKey(i => i.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.AnalisesDrone)
            .WithOne(a => a.Plantacao)
            .HasForeignKey(a => a.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.MissoesDrone)
            .WithOne(m => m.Plantacao)
            .HasForeignKey(m => m.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Ocorrencias)
            .WithOne(o => o.Plantacao)
            .HasForeignKey(o => o.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}