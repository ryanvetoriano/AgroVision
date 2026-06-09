using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class MissaoDroneConfiguration : IEntityTypeConfiguration<MissaoDrone>
{
    public void Configure(EntityTypeBuilder<MissaoDrone> builder)
    {
        builder.ToTable("TB_MISSAO_DRONE_GS");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("ID_MISSAO_DRONE")
            .ValueGeneratedOnAdd();

        builder.Property(m => m.DroneId)
            .HasColumnName("ID_DRONE")
            .IsRequired();

        builder.Property(m => m.PlantacaoId)
            .HasColumnName("ID_PLANTACAO")
            .IsRequired();

        builder.Property(m => m.DataInicio)
            .HasColumnName("DATA_INICIO")
            .IsRequired();

        builder.Property(m => m.DataFim)
            .HasColumnName("DATA_FIM")
            .IsRequired(false);

        builder.Property(m => m.AreaMapeada)
            .HasColumnName("AREA_MAPEADA")
            .HasColumnType("NUMBER(15,2)")
            .IsRequired();

        builder.Property(m => m.AltitudeMedia)
            .HasColumnName("ALTITUDE_MEDIA")
            .HasColumnType("NUMBER(10,2)")
            .IsRequired();

        builder.Property(m => m.Status)
            .HasColumnName("STATUS_MISSAO")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(m => m.Drone)
            .WithMany(d => d.Missoes)
            .HasForeignKey(m => m.DroneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Plantacao)
            .WithMany(p => p.MissoesDrone)
            .HasForeignKey(m => m.PlantacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.LeiturasSensor)
            .WithOne(l => l.MissaoDrone)
            .HasForeignKey(l => l.MissaoDroneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}